using UnityEngine;

public class random_sc : MonoBehaviour
{
    public static int random_gacha(int int_temp)//��� ��� �� ����ŭ ����  �ִ� 3�� Ȯ���� ���� ���� ������ ������ 
    {
        float i = Random.Range(1,101);
        if (int_temp == 2)// 90�� 10
        {
            if (i < 90)
            {
                return 0;
            }
            else
                return 1;
        }
        else if (int_temp == 3)// 70 15 15 Ȯ�� �������  
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
