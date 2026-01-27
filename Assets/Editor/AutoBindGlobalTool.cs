using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using Core.Architecture;

public static class AutoBindGlobalTool
{
	// 相似度阈值
	private const float FuzzyThreshold = 0.6f;

	// 核心黑科技：这就话给所有 MonoBehaviour 的右键菜单加了按钮
	[MenuItem("CONTEXT/MonoBehaviour/🛠️ Auto Bind (智能关联)")]
	private static void AutoBindFromInspector(MenuCommand command)
	{
		// 获取你当前右键点击的那个脚本对象
		MonoBehaviour target = command.context as MonoBehaviour;
		if (target == null) return;

		DoAutoBind(target);
	}

	
	public static void DoAutoBind(MonoBehaviour target)
    {
        Undo.RecordObject(target, "Auto Bind UI");

        var fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var allChildren = new List<Transform>(target.GetComponentsInChildren<Transform>(true));
        
        var childMap = new Dictionary<string, Transform>();
        foreach (var child in allChildren)
        {
            if (!childMap.ContainsKey(child.name)) childMap.Add(child.name, child);
        }

        int count = 0;

        foreach (var field in fields)
        {
            var attr = field.GetCustomAttribute<AutoBindAttribute>();
            if (attr == null) continue;

            string targetName = string.IsNullOrEmpty(attr.Name) ? field.Name : attr.Name;
            
            // --- 特殊处理：如果你明确写了 [AutoBind("Self")] ---
            if (targetName.Equals("Self", StringComparison.OrdinalIgnoreCase))
            {
                if (BindComponent(target, field, target.transform)) count++;
                continue;
            }

            Transform bestMatch = null;
            float bestScore = 0f;

            // Step 1: 精确匹配名字
            if (childMap.TryGetValue(targetName, out Transform exact))
            {
                bestMatch = exact;
            }
            // Step 2: 模糊匹配名字
            else
            {
                foreach (var child in allChildren)
                {
                    float score = GetSimilarity(targetName, child.name);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMatch = child;
                    }
                }
                // 只有相似度合格才认
                if (bestScore < FuzzyThreshold) bestMatch = null;
            }

            // Step 3: 【关键修复】保底检查自己
            // 如果子物体里实在找不到名字匹配的，
            // 且我自己身上刚好有这个类型的组件，那就认为你是想绑自己
            if (bestMatch == null)
            {
                var selfComp = target.GetComponent(field.FieldType);
                if (selfComp != null)
                {
                    field.SetValue(target, selfComp);
                    Debug.Log($"<color=cyan>[AutoBind] 名字没对上，但在【根物体】上找到了组件: {field.FieldType.Name}</color>");
                    count++;
                    continue; // 成功了，跳过后续逻辑
                }
            }

            // 执行绑定 (针对 Step 1 和 Step 2 的结果)
            if (bestMatch != null)
            {
                if (BindComponent(target, field, bestMatch)) count++;
                if (bestScore > 0) Debug.LogWarning($"模糊匹配: {targetName} -> {bestMatch.name}");
            }
            else
            {
                Debug.LogError($"[AutoBind] 找不到: {targetName} (且根物体上也没有)");
            }
        }
        
        EditorUtility.SetDirty(target);
        Debug.Log($"绑定结束: {count}");
    }

    // 抽离出来的绑定小函数
    private static bool BindComponent(MonoBehaviour target, FieldInfo field, Transform sourceTf)
    {
        if (field.FieldType == typeof(GameObject))
        {
            field.SetValue(target, sourceTf.gameObject);
            return true;
        }
        else
        {
            var component = sourceTf.GetComponent(field.FieldType);
            if (component != null)
            {
                field.SetValue(target, component);
                return true;
            }
        }
        return false;
    }


	private static float GetSimilarity(string s, string t)
	{
		return StringAlgo.GetSimilarity(s, t);
	}
}

/// <summary>
/// 字符串算法工具类
/// </summary>
public static class StringAlgo
{
	/// <summary> 计算相似度 0.0 ~ 1.0 (1.0 = 完全一样)</summary>
	public static float GetSimilarity(string source, string target)
	{
		if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target)) return 0f;
		if (source == target) return 1f;

		int steps = ComputeLevenshteinDistance(source, target);
		return 1.0f - ((float)steps / Mathf.Max(source.Length, target.Length));
	}

	private static int ComputeLevenshteinDistance(string s, string t)
	{
		int n = s.Length;
		int m = t.Length;
		int[,] d = new int[n + 1, m + 1];

		if (n == 0) return m;
		if (m == 0) return n;

		for (int i = 0; i <= n; d[i, 0] = i++)
		{
		}

		for (int j = 0; j <= m; d[0, j] = j++)
		{
		}

		for (int i = 1; i <= n; i++)
		{
			for (int j = 1; j <= m; j++)
			{
				int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
				d[i, j] = Math.Min(
					Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
					d[i - 1, j - 1] + cost);
			}
		}

		return d[n, m];
	}
}