using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI
{

    // 道岔类
    public class Switch
    {
        public string name;
        public int state;  //0-定位 1-反位（用于确定存储进路时道岔的位置）
        public int error;  //0-无故障 1-道岔四开 2-道岔失表
        public Boolean DBJ;  //false-落下 true-吸起
        public Boolean FBJ;  //false-落下 true-吸起
        public Boolean locked;  //false-未单锁 true-单锁
        public Boolean SJ;  //false-落下-道岔锁闭 true-吸起-道岔空闲
        public Boolean blocked;  //false-未封锁 true-封锁
        public TrackSeg seg;  //记录道岔所在轨道区段
        public Switch pre_w;
        public Switch next_w;
        public TrackSeg pre_t;
        public TrackSeg next_t;
        public Signal pre_s;
        public Signal next_s;

    }

    //轨道区段类
    public class TrackSeg
    {
        public string name;
        public int state;  //0-空闲 1-有车占用(用于解锁) 2-有车占用(用于右键菜单设置)
        public int error;  //0-无故障 1-故障占用
        public Boolean LJ_1, LJ_2;  //LJ_1=LJ_2=false-表示锁闭状态，LJ_1=LJ_2=true-表示解锁  （用于解锁）
        public Boolean locked;  //false-未锁闭（解锁） true-区段锁闭
        public Switch pre_w;
        public Switch next_w;
        public Signal pre_s;
        public Signal next_s;
    }

    //信号机类
    public class Signal
    {
        public string name;
        public Boolean pressed;  //false-信号机未被按下 true-信号机被按下
        public Boolean XJ;  //false-落下-信号未开放 true-吸起-信号开放
        public Boolean DJ;  //false-灯丝断丝 true-灯丝正常
        public TrackSeg approach_seg;  //存储信号机接近区段
        public TrackSeg pre_t;
        public TrackSeg next_t;
        public Switch pre_w;
        public Switch next_w;
    }

    public class Ptr
    {
        public TrackSeg next_t;
        public Switch next_w;
        public Signal next_s;
    }

}
