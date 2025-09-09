using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{

    public static UnityAction<GameObject, int> characterDamaged;
    // 🔹 Khai báo một sự kiện toàn cục (static) kiểu UnityAction
    // 🔹 Khi gọi Invoke, sẽ truyền vào: 
    //     - GameObject (nhân vật bị trúng đòn) 
    //     - int (số damage nhận được)

    public static UnityAction<GameObject, int> characterHealed;
    // 🔹 Khai báo một sự kiện toàn cục khác cho việc hồi máu
    // 🔹 Khi gọi Invoke, sẽ truyền vào: 
    //     - GameObject (nhân vật được hồi máu) 
    //     - int (số máu được hồi)
}
