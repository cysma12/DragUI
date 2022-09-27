using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleObjectManager : MonoBehaviour
{
    /*singleObject ����
     * �뵵 : ��� ������ �ʿ��� �̱��� ��ũ��Ʈ ����  
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
