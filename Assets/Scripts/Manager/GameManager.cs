using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        
        private void Start()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("Prefabs/Monster");
        }
  
    }
}