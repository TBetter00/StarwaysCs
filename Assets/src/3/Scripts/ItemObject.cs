using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item ข้อมูลไอเท็ม;
    /// <summary>
    /// 
    /// </summary>
    /// <returns>ค่า Item ของ ตัวมันเอง</returns>
    public Item Collected()
    {
        if (ข้อมูลไอเท็ม != null)
        {
            return ข้อมูลไอเท็ม;
        }
        return null;
    }
    /*
    สารธารณะ ไอเท็ม เก็บ(){
        ถ้า (ข้อมูลไอเท็ม != เป็นศูนย์)
        {
            ย้อนกลับ ข้อมูลไอเท็ม;
        }
        ย้อนกลับ เป็นศูนย์;
    }
    */
}
