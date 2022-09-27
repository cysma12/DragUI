using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleObjectManager : MonoBehaviour
{
    /*singleObject 설계
     * 용도 : 모든 씬에서 필요한 싱글턴 스크립트 관리  
     * 
     */

    public static SingleObjectManager Instance;
      
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }            
    }


}
