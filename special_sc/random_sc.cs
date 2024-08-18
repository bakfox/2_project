using UnityEngine;

public class random_sc : MonoBehaviour
{
    public static int random_gacha(int int_temp)//사용 방법 들어간 수만큼 넣음  최대 3개 확률은 먼저 넣은 순서로 정해짐 
    {
        float i = Random.Range(1,101);
        if (int_temp == 2)// 90대 10
        {
            if (i < 90)
            {
                return 0;
            }
            else
                return 1;
        }
        else if (int_temp == 3)// 70 15 15 확률 순서대로  
        {
            if (i < 70)
            {
                return 0;
            }
            else if (i < 84 && i >= 70)
            {
                return 1;
            }
            else
                return 2;
        }
        else
            return 0;
    }
}
