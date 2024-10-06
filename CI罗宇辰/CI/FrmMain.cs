using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CI
{
    public partial class FrmMain : Form
    {
        //实例化站场设备对象
        // 1.信号机
        Signal S = new Signal();
        Signal S_F = new Signal();
        Signal X_3 = new Signal();
        Signal X_I = new Signal();
        Signal X_II = new Signal();
        Signal X_4 = new Signal();
        Signal X_6 = new Signal();
        Signal X_8 = new Signal();
        Signal XD_2 = new Signal();
        Signal XD_4 = new Signal();
        Signal XD_6 = new Signal();
        Signal XD_8 = new Signal();

        //Signal begin = new Signal();  //记录按下始端按钮的信号机
        //Signal end = new Signal();  //记录按下终端按钮的信号机
        // 2.轨道区段
        TrackSeg S_approach = new TrackSeg();
        TrackSeg SF_approach = new TrackSeg();
        TrackSeg DG_IBG = new TrackSeg();
        TrackSeg DG_2 = new TrackSeg();
        TrackSeg DG_4 = new TrackSeg();
        TrackSeg DG_8 = new TrackSeg();
        TrackSeg DG_6_14 = new TrackSeg();
        TrackSeg DG_12 = new TrackSeg();
        TrackSeg DG_16 = new TrackSeg();
        TrackSeg DG_3G = new TrackSeg();
        TrackSeg DG_IG = new TrackSeg();
        TrackSeg DG_IIG = new TrackSeg();
        TrackSeg DG_4G = new TrackSeg();
        TrackSeg DG_6G = new TrackSeg();
        TrackSeg DG_8G = new TrackSeg();
        // 3.道岔
        Switch D_2 = new Switch();
        Switch D_4 = new Switch();
        Switch D_6 = new Switch();
        Switch D_8 = new Switch();
        Switch D_10 = new Switch();
        Switch D_12 = new Switch();
        Switch D_14 = new Switch();
        Switch D_16 = new Switch();

        Ptr ptr = new Ptr();
        int time_180 = 5;
        int time_30 = 3;
        int[] type = new int[12] {4,4,4,4,4,4,4,4,4,4,4,4};  //0-接车 1-发车 2-引导 3-引导总锁 4-进路不存在 5-调车(往左) 6-调车（往右）
        Boolean[] road_exist = new Boolean[12];  //进路是否办理
        public FrmMain()
        {
            //站场图绘制数据存入
            InitializeComponent();
        }
        //输出信息到屏幕上
        private void OutMsg(RichTextBox rtb, string msg, Color color)
        {
            rtb.Invoke(new EventHandler(delegate
            {
                rtb.SelectionStart = rtb.Text.Length;//设置插入符位置为文本框末
                rtb.SelectionColor = color;//设置文本颜色
                rtb.AppendText(msg + "\r\n");//输出文本，换行
                rtb.ScrollToCaret();//滚动条滚到到最新插入行
            }));
        }
        //根据各进路建立进路链表型数据结构（双向链表）
        //S->XII列车进路type[0]
        public void BuildDataList_S_XII()
        {
            S.next_w = D_4; S.next_t = null; S.pre_t = null; S.pre_w = null;
            D_4.next_t = DG_4; D_4.next_w = null; D_4.next_s = null; D_4.pre_s = S; D_4.pre_t = null; D_4.pre_w = null; D_4.state = 0;
            DG_4.next_s = XD_6; DG_4.next_w = null; DG_4.pre_w = D_4; DG_4.pre_s = null;
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_10; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 0;
            D_10.next_t = DG_16; D_10.next_s = null; D_10.next_w = null; D_10.pre_w = D_6; D_10.pre_s = null; D_10.pre_t = null; D_10.state = 0;
            DG_16.next_w = D_16; DG_16.next_s = null; DG_16.pre_w = D_10; DG_16.pre_s = null;
            D_16.next_s = X_II; D_16.next_t = null; D_16.next_w = null; D_16.pre_t = DG_16; D_16.pre_s = null; D_16.pre_w = null; D_16.state = 0;
            X_II.next_t = DG_IIG; X_II.next_w = null; X_II.pre_w = D_16; X_II.pre_t = null;
            DG_IIG.next_w = null; DG_IIG.next_s = null; DG_IIG.pre_w = null; DG_IIG.pre_s = X_II;
        }
        //S->X8列车进路type[1]
        public void BuildDataList_S_X8()
        {
            S.next_w = D_4; S.next_t = null; S.pre_t = null; S.pre_w = null;
            D_4.next_t = DG_4; D_4.next_w = null; D_4.next_s = null; D_4.pre_s = S; D_4.pre_t = null; D_4.pre_w = null; D_4.state = 0;
            DG_4.next_s = XD_6; DG_4.next_w = null; DG_4.pre_w = D_4; DG_4.pre_s = null;
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_10; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 0;
            D_10.next_w = D_14; D_10.next_s = null; D_10.next_t = null; D_10.pre_w = D_6; D_10.pre_s = null; D_10.pre_t = null; D_10.state = 1;
            D_14.next_s = X_8; D_14.next_w = null; D_14.next_t = null; D_14.pre_w = D_10; D_14.pre_t = null; D_14.pre_s = null; D_14.state = 1;
            X_8.next_t = DG_8G; X_8.next_w = null; X_8.pre_w = D_14; X_8.pre_t = null;
            DG_8G.next_w = null; DG_8G.next_s = null; DG_8G.pre_s = X_8; DG_8G.pre_w = null;
        }
        //S->X3列车进路type[2]
        public void BuildDataList_S_X3()
        {
            S.next_w = D_4; S.next_t = null; S.pre_t = null; S.pre_w = null;
            D_4.next_t = DG_4; D_4.next_w = null; D_4.next_s = null; D_4.pre_s = S; D_4.pre_t = null; D_4.pre_w = null; D_4.state = 0;
            DG_4.next_s = XD_6; DG_4.next_w = null; DG_4.pre_w = D_4; DG_4.pre_s = null;
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_8; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 1;
            D_8.next_t = DG_8; D_8.next_s = null; D_8.next_w = null; D_8.pre_w = D_6; D_8.pre_s = null; D_8.pre_t = null; D_8.state = 1;
            DG_8.next_s = XD_8; DG_8.next_w = null; DG_8.pre_w = D_8; DG_8.pre_s = null;
            XD_8.next_t = DG_12; XD_8.next_w = null; XD_8.pre_t = DG_8; XD_8.pre_w = null;
            DG_12.next_w = D_12; DG_12.next_s = null; DG_12.pre_s = XD_8; DG_12.pre_w = null;
            D_12.next_s = X_3; D_12.next_t = null; D_12.next_w = null; D_12.pre_t = DG_12; D_12.pre_s = null; D_12.pre_w = null; D_12.state = 1;
            X_3.next_t = DG_3G; X_3.next_w = null; X_3.pre_w = D_12; X_3.pre_t = null;
            DG_3G.next_w = null; DG_3G.next_s = null; DG_3G.pre_s = X_3; DG_3G.pre_w = null;
        }
        //S->XI列车进路type[3]
        public void BuildDataList_S_XI()
        {
            S.next_w = D_4; S.next_t = null; S.pre_t = null; S.pre_w = null;
            D_4.next_t = DG_4; D_4.next_w = null; D_4.next_s = null; D_4.pre_s = S; D_4.pre_t = null; D_4.pre_w = null; D_4.state = 0;
            DG_4.next_s = XD_6; DG_4.next_w = null; DG_4.pre_w = D_4; DG_4.pre_s = null;
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_8; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 1;
            D_8.next_t = DG_8; D_8.next_s = null; D_8.next_w = null; D_8.pre_w = D_6; D_8.pre_s = null; D_8.pre_t = null; D_8.state = 1;
            DG_8.next_s = XD_8; DG_8.next_w = null; DG_8.pre_w = D_8; DG_8.pre_s = null;
            XD_8.next_t = DG_12; XD_8.next_w = null; XD_8.pre_t = DG_8; XD_8.pre_w = null;
            DG_12.next_w = D_12; DG_12.next_s = null; DG_12.pre_s = XD_8; DG_12.pre_w = null;
            D_12.next_s = X_I; D_12.next_t = null; D_12.next_w = null; D_12.pre_t = DG_12; D_12.pre_s = null; D_12.pre_w = null; D_12.state = 0;
            X_I.next_t = DG_IG; X_I.next_w = null; X_I.pre_t = DG_12; X_I.pre_w = null;
            DG_IG.next_w = null; DG_IG.next_s = null; DG_IG.pre_s = X_I; DG_IG.pre_w = null;
        }
        //S->X4列车进路type[4]
        public void BuildDataList_S_X4()
        {
            S.next_w = D_4; S.next_t = null; S.pre_t = null; S.pre_w = null;
            D_4.next_t = DG_4; D_4.next_w = null; D_4.next_s = null; D_4.pre_s = S; D_4.pre_t = null; D_4.pre_w = null; D_4.state = 0;
            DG_4.next_s = XD_6; DG_4.next_w = null; DG_4.pre_w = D_4; DG_4.pre_s = null;
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_10; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 0;
            D_10.next_t = DG_16; D_10.next_s = null; D_10.next_w = null; D_10.pre_w = D_6; D_10.pre_s = null; D_10.pre_t = null; D_10.state = 0;
            DG_16.next_w = D_16; DG_16.next_s = null; DG_16.pre_w = D_10; DG_16.pre_s = null;
            D_16.next_s = X_4; D_16.next_t = null; D_16.next_w = null; D_16.pre_t = DG_16; D_16.pre_s = null; D_16.pre_w = null; D_16.state = 1;
            X_4.next_t = DG_4G; X_4.next_w = null; X_4.pre_w = D_16; X_4.pre_t = null;
            DG_4G.next_w = null; DG_4G.next_s = null; DG_4G.pre_s = X_4; DG_4G.pre_w = null;
        }
        //S->X6列车进路type[5]
        public void BuildDataList_S_X6()
        {
            S.next_w = D_4; S.next_t = null; S.pre_t = null; S.pre_w = null;
            D_4.next_t = DG_4; D_4.next_w = null; D_4.next_s = null; D_4.pre_s = S; D_4.pre_t = null; D_4.pre_w = null; D_4.state = 0;
            DG_4.next_s = XD_6; DG_4.next_w = null; DG_4.pre_w = D_4; DG_4.pre_s = null;
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_10; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 0;
            D_10.next_w = D_14; D_10.next_s = null; D_10.next_t = null; D_10.pre_w = D_6; D_10.pre_s = null; D_10.pre_t = null; D_10.state = 1;
            D_14.next_s = X_6; D_14.next_w = null; D_14.next_t = null; D_14.pre_w = D_10; D_14.pre_t = null; D_14.pre_s = null; D_14.state = 0;
            X_6.next_t = DG_6G; X_6.next_w = null; X_6.pre_w = D_14; X_6.pre_t = null;
            DG_6G.next_w = null; DG_6G.next_s = null; DG_6G.pre_s = X_6; DG_6G.pre_w = null;
        }
        //D6->D8调车进路type[6]
        public void BuildDataList_D6_D8()
        {
            XD_6.next_t = DG_6_14; XD_6.next_w = null; XD_6.pre_t = DG_4; XD_6.pre_w = null;
            DG_6_14.next_w = D_6; DG_6_14.next_s = null; DG_6_14.pre_s = XD_6; DG_6_14.pre_w = null;
            D_6.next_w = D_8; D_6.next_s = null; D_6.next_t = null; D_6.pre_t = DG_6_14; D_6.pre_s = null; D_6.pre_w = null; D_6.state = 1;
            D_8.next_t = DG_8; D_8.next_s = null; D_8.next_w = null; D_8.pre_w = D_6; D_8.pre_s = null; D_8.pre_t = null; D_8.state = 1;
            DG_8.next_s = XD_8; DG_8.next_w = null; DG_8.pre_w = D_8; DG_8.pre_s = null;
            XD_8.next_t = DG_12; XD_8.next_w = null; XD_8.pre_t = DG_8; XD_8.pre_w = null;
        }
        //联锁逻辑检查函数
        public int InterloackingCheck(Signal begin_btn, Signal end_btn, int Direction)  //Direction=0表示接车进路，1表示发车进路
        {
            if(Direction == 0)
            {
                ptr.next_s = begin_btn; ptr.next_w = null; ptr.next_t = null;
                while (ptr.next_w != null || ptr.next_s != null || ptr.next_t != null)
                {
                    //判断指针是否指向轨道区段类
                    if (ptr.next_t != null)
                    {
                        //轨道区段是否故障或占用
                        if (ptr.next_t.error == 1)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_t.name + "故障占用！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_t.state == 2)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_t.name + "有车占用！", Color.Red);
                            return 0;
                        }
                        else
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_t.name + "检查完毕！", Color.Black);
                            //进行下一节点类型判断
                            if (ptr.next_t.next_s != null)  //判断下一节点是否为信号机类
                            {
                                ptr.next_s = ptr.next_t.next_s;
                                ptr.next_t = null;
                                ptr.next_w = null;
                                continue;
                            }
                            if (ptr.next_t.next_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_t.next_w;
                                ptr.next_s = null;
                                ptr.next_t = null;

                                continue;
                            }
                        }

                    }
                    //判断指针是否指向道岔类
                    if (ptr.next_w != null)
                    {
                        if(ptr.next_w.error == 2)  //判断道岔是否失表
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "失表！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_w.error == 1)  //判断道岔是否四开
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "处于四开状态！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_w.blocked == true)  //判断道岔是否封锁
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "已封锁！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_w.locked == true)  //判断道岔是否单锁
                        {
                            if ((ptr.next_w.state == 0 && ptr.next_w.DBJ == true && ptr.next_w.FBJ == false) || (ptr.next_w.state == 1 && ptr.next_w.FBJ == true && ptr.next_w.DBJ == false))
                            {
                                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "已单锁，道岔位置符合进路要求！", Color.Black);
                                //进行下一节点类型判断
                                if (ptr.next_w.next_t != null)  //判断下一节点是否为轨道区段类
                                {
                                    ptr.next_t = ptr.next_w.next_t;
                                    ptr.next_s = null;
                                    ptr.next_w = null;
                                    continue;
                                }
                                if (ptr.next_w.next_s != null)  //判断下一节点是否为信号机类
                                {
                                    ptr.next_s = ptr.next_w.next_s;
                                    ptr.next_t = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.next_w != null)  //判断下一节点是否为道岔类
                                {
                                    ptr.next_w = ptr.next_w.next_w;
                                    ptr.next_s = null;
                                    ptr.next_t = null;
                                    continue;
                                }

                            }
                            else
                            {
                                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "已单锁，道岔位置不符合进路要求！", Color.Red);
                                return 0;
                            }
                        }
                        else
                        {
                            if (ptr.next_w.state == 0)  //进路要求道岔在定位
                            {
                                if (ptr.next_w.DBJ == true && ptr.next_w.FBJ == false)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置正确！", Color.Black);
                                }
                                if (ptr.next_w.DBJ == false && ptr.next_w.FBJ == true)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置不一致，需进行道岔转换！", Color.Black);
                                    ptr.next_w.DBJ = true; ptr.next_w.FBJ = false;
                                }
                                //进行下一节点类型判断
                                if (ptr.next_w.next_t != null)  //判断下一节点是否为轨道区段类
                                {
                                    ptr.next_t = ptr.next_w.next_t;
                                    continue;
                                }

                                if (ptr.next_w.next_s != null)  //判断下一节点是否为信号机类
                                {
                                    ptr.next_s = ptr.next_w.next_s;
                                    ptr.next_t = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.next_w != null)  //判断下一节点是否为道岔类
                                {
                                    ptr.next_w = ptr.next_w.next_w;
                                    ptr.next_s = null;
                                    ptr.next_t = null;
                                    continue;
                                }

                            }
                            if (ptr.next_w.state == 1)  //进路要求道岔在反位
                            {
                                if (ptr.next_w.DBJ == false && ptr.next_w.FBJ == true)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置正确！", Color.Black);
                                }
                                if (ptr.next_w.DBJ == true && ptr.next_w.FBJ == false)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置不一致，需进行道岔转换！", Color.Black);
                                    ptr.next_w.DBJ = false; ptr.next_w.FBJ = true;
                                }
                                //进行下一节点类型判断
                                if (ptr.next_w.next_t != null)  //判断下一节点是否为轨道区段类
                                {
                                    ptr.next_t = ptr.next_w.next_t;
                                    ptr.next_s = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.next_s != null)  //判断下一节点是否为信号机类
                                {
                                    ptr.next_s = ptr.next_w.next_s;
                                    ptr.next_t = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.next_w != null)  //判断下一节点是否为道岔类
                                {
                                    ptr.next_w = ptr.next_w.next_w;
                                    ptr.next_s = null;
                                    ptr.next_t = null;
                                    continue;
                                }
                            }
                        }
                    }
                    //判断指针是否指向信号机类
                    if (ptr.next_s != null)
                    {
                        if (ptr.next_s.DJ == false)  //判断灯丝是否断丝
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_s.name + "灯丝断丝！", Color.Red);
                            return 0;
                        }
                        else if(ptr.next_s.XJ == true)  //检查敌对信号
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_s.name + "敌对信号开放！", Color.Red);
                            return 0;
                        }
                        if (ptr.next_s == end_btn) //若指针搜索到了终端按钮，则检查完终端信号机后结束联锁检查
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "该进路联锁条件检查完毕！", Color.Green);
                            return 1;
                        }
                        else
                        {
                            //进行下一节点类型判断
                            if (ptr.next_s.next_t != null)  //判断下一节点是否为轨道区段类
                            {
                                ptr.next_t = ptr.next_s.next_t;
                                ptr.next_s = null;
                                ptr.next_w = null;
                                continue;
                            }

                            if (ptr.next_s.next_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_s.next_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }
                    }
                }
                return 1;
            }
            else if(Direction == 1)
            {
                ptr.next_s = begin_btn; ptr.next_w = null; ptr.next_t = null;
                while (ptr.next_w != null || ptr.next_s != null || ptr.next_t != null)
                {
                    //判断指针是否指向轨道区段类
                    if (ptr.next_t != null)
                    {
                        //轨道区段是否故障或占用
                        if (ptr.next_t.error == 1)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_t.name + "故障占用！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_t.state == 2)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_t.name + "有车占用！", Color.Red);
                            return 0;
                        }
                        else
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_t.name + "检查完毕！", Color.Black);
                            //进行下一节点类型判断
                            if (ptr.next_t.pre_s != null)  //判断下一节点是否为信号机类
                            {
                                ptr.next_s = ptr.next_t.pre_s;
                                ptr.next_t = null;
                                ptr.next_w = null;
                                continue;
                            }
                            if (ptr.next_t.pre_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_t.pre_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }

                    }
                    //判断指针是否指向道岔类
                    if (ptr.next_w != null)
                    {
                        if (ptr.next_w.error == 2)  //判断道岔是否失表
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "失表！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_w.error == 1)  //判断道岔是否四开
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "处于四开状态！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_w.blocked == true)  //判断道岔是否封锁
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "已封锁！", Color.Red);
                            return 0;

                        }
                        else if (ptr.next_w.locked == true)  //判断道岔是否单锁
                        {
                            if ((ptr.next_w.state == 0 && ptr.next_w.DBJ == true && ptr.next_w.FBJ == false) || (ptr.next_w.state == 1 && ptr.next_w.FBJ == true && ptr.next_w.DBJ == false))
                            {
                                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "已单锁，道岔位置符合进路要求！", Color.Black);
                                //进行下一节点类型判断
                                if (ptr.next_w.pre_t != null)  //判断下一节点是否为轨道区段类
                                {
                                    ptr.next_t = ptr.next_w.pre_t;
                                    ptr.next_s = null;
                                    ptr.next_w = null;
                                    continue;
                                }
                                if (ptr.next_w.pre_s != null)  //判断下一节点是否为信号机类
                                {
                                    ptr.next_s = ptr.next_w.pre_s;
                                    ptr.next_t = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.pre_w != null)  //判断下一节点是否为道岔类
                                {
                                    ptr.next_w = ptr.next_w.pre_w;
                                    ptr.next_s = null;
                                    ptr.next_t = null;
                                    continue;
                                }

                            }
                            else
                            {
                                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "已单锁，道岔位置不符合进路要求！", Color.Red);
                                return 0;
                            }
                        }
                        else
                        {
                            if (ptr.next_w.state == 0)  //进路要求道岔在定位
                            {
                                if (ptr.next_w.DBJ == true && ptr.next_w.FBJ == false)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置正确！", Color.Black);
                                }
                                if (ptr.next_w.DBJ == false && ptr.next_w.FBJ == true)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置不一致，需进行道岔转换！", Color.Black);
                                    ptr.next_w.DBJ = true; ptr.next_w.FBJ = false;
                                }
                                //进行下一节点类型判断
                                if (ptr.next_w.pre_t != null)  //判断下一节点是否为轨道区段类
                                {
                                    ptr.next_t = ptr.next_w.pre_t;
                                    continue;
                                }

                                if (ptr.next_w.pre_s != null)  //判断下一节点是否为信号机类
                                {
                                    ptr.next_s = ptr.next_w.pre_s;
                                    ptr.next_t = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.pre_w != null)  //判断下一节点是否为道岔类
                                {
                                    ptr.next_w = ptr.next_w.pre_w;
                                    ptr.next_s = null;
                                    ptr.next_t = null;
                                    continue;
                                }

                            }
                            if (ptr.next_w.state == 1)  //进路要求道岔在反位
                            {
                                if (ptr.next_w.DBJ == false && ptr.next_w.FBJ == true)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置正确！", Color.Black);
                                }
                                if (ptr.next_w.DBJ == true && ptr.next_w.FBJ == false)
                                {
                                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_w.name + "位置不一致，需进行道岔转换！", Color.Black);
                                    ptr.next_w.DBJ = false; ptr.next_w.FBJ = true;
                                }
                                //进行下一节点类型判断
                                if (ptr.next_w.pre_t != null)  //判断下一节点是否为轨道区段类
                                {
                                    ptr.next_t = ptr.next_w.pre_t;
                                    ptr.next_s = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.pre_s != null)  //判断下一节点是否为信号机类
                                {
                                    ptr.next_s = ptr.next_w.pre_s;
                                    ptr.next_t = null;
                                    ptr.next_w = null;
                                    continue;
                                }

                                if (ptr.next_w.pre_w != null)  //判断下一节点是否为道岔类
                                {
                                    ptr.next_w = ptr.next_w.pre_w;
                                    ptr.next_s = null;
                                    ptr.next_t = null;
                                    continue;
                                }
                            }
                        }
                    }
                    //判断指针是否指向信号机类
                    if (ptr.next_s != null)
                    {
                        if (ptr.next_s.DJ == false)  //判断灯丝是否断丝
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_s.name + "灯丝断丝！", Color.Red);
                            return 0;
                        }
                        else if (ptr.next_s.XJ == true)  //检查敌对信号
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + ptr.next_s.name + "敌对信号开放！", Color.Red);
                            return 0;
                        }
                        if (ptr.next_s == end_btn) //若指针搜索到了终端按钮，则检查完终端信号机后结束联锁检查
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "该进路联锁条件检查完毕！", Color.Green);
                            return 1;
                        }
                        else
                        {
                            //进行下一节点类型判断
                            if (ptr.next_s.pre_t != null)  //判断下一节点是否为轨道区段类
                            {
                                ptr.next_t = ptr.next_s.pre_t;
                                ptr.next_s = null;
                                ptr.next_w = null;
                                continue;
                            }

                            if (ptr.next_s.pre_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_s.pre_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }
                    }
                }
                return 1;
            }
            else
            {
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "Direction参数为0或1！", Color.Red);
                return 0;
            }
        }
        //联锁检查完成进路建立
        public void BuildRoad(Signal begin_btn, Signal end_btn, int Direction)
        {
            if (Direction == 0)
            {
                ptr.next_s = begin_btn; ptr.next_w = null; ptr.next_t = null;
                while (ptr.next_w != null || ptr.next_s != null || ptr.next_t != null)
                {
                    //判断指针是否指向轨道区段类
                    if (ptr.next_t != null)
                    {
                        //锁闭轨道区段
                        ptr.next_t.locked = true; ptr.next_t.LJ_1 = false;ptr.next_t.LJ_2 = false;
                        //进行下一节点类型判断
                        if (ptr.next_t.next_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_t.next_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }
                        if (ptr.next_t.next_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_t.next_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }
                    }
                    //判断指针是否指向道岔类
                    if (ptr.next_w != null)
                    {
                        //确保双动道岔一致变换
                        DoubleSwitchChange();
                        //锁闭道岔
                        ptr.next_w.SJ = false;
                        //进行下一节点类型判断
                        if (ptr.next_w.next_t != null)  //判断下一节点是否为轨道区段类
                        {
                            ptr.next_t = ptr.next_w.next_t;
                            continue;
                        }

                        if (ptr.next_w.next_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_w.next_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }

                        if (ptr.next_w.next_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_w.next_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }
                        
                    }
                    //判断指针是否指向信号机类
                    if (ptr.next_s != null)
                    {
                        //开放信号显示
                        ptr.next_s.XJ = true;
                        if (ptr.next_s == end_btn) //若指针搜索到了终端按钮，则进路建立完成
                        {
                            ptr.next_s.next_t.locked = true; ptr.next_s.next_t.LJ_1 = false; ptr.next_s.next_t.LJ_2 = false;
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + begin_btn.name + "至" + end_btn.name + "进路已成功建立！", Color.Green);
                            break;
                        }
                        else
                        {
                            //进行下一节点类型判断
                            if (ptr.next_s.next_t != null)  //判断下一节点是否为轨道区段类
                            {
                                ptr.next_t = ptr.next_s.next_t;
                                ptr.next_s = null;
                                ptr.next_w = null;
                                continue;
                            }
                            if (ptr.next_s.next_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_s.next_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }
                    }
                }
                //更新站场显示
                UpdateSignal(); UpdateSwitch(); UpdateTrack();
                //开放模拟行车按钮功能
                Btn_MNXC.Enabled = true;

            }
            else if (Direction == 1)
            {
                ptr.next_s = begin_btn; ptr.next_w = null; ptr.next_t = null;
                while (ptr.next_w != null || ptr.next_s != null || ptr.next_t != null)
                {
                    //判断指针是否指向轨道区段类
                    if (ptr.next_t != null)
                    {
                        //锁闭轨道区段
                        ptr.next_t.locked = true; ptr.next_t.LJ_1 = false; ptr.next_t.LJ_2 = false;
                        //进行下一节点类型判断
                        if (ptr.next_t.pre_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_t.pre_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }
                        if (ptr.next_t.pre_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_t.pre_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }
                    }
                    //判断指针是否指向道岔类
                    if (ptr.next_w != null)
                    {
                        //确保双动道岔一致变换
                        DoubleSwitchChange();
                        //锁闭道岔
                        ptr.next_w.SJ = false;
                        //进行下一节点类型判断
                        if (ptr.next_w.pre_t != null)  //判断下一节点是否为轨道区段类
                        {
                            ptr.next_t = ptr.next_w.pre_t;
                            continue;
                        }

                        if (ptr.next_w.pre_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_w.pre_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }

                        if (ptr.next_w.pre_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_w.pre_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }

                    }
                    //判断指针是否指向信号机类
                    if (ptr.next_s != null)
                    {
                        //开放信号显示
                        ptr.next_s.XJ = true;
                        if (ptr.next_s == end_btn) //若指针搜索到了终端按钮，则进路建立完成
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + begin_btn.name + "至" + end_btn.name + "进路已成功建立！", Color.Green);
                            break;
                        }
                        else
                        {
                            //进行下一节点类型判断
                            if (ptr.next_s.pre_t != null)  //判断下一节点是否为轨道区段类
                            {
                                ptr.next_t = ptr.next_s.pre_t;
                                ptr.next_s = null;
                                ptr.next_w = null;
                                continue;
                            }
                            if (ptr.next_s.pre_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_s.pre_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }
                    }
                }
                //更新站场显示
                UpdateSignal(); UpdateSwitch(); UpdateTrack();
                //开放模拟行车按钮功能
                Btn_MNXC.Enabled = true;
            }
        }
        //进路取消
        public void CancelRoad(Signal begin_btn, Signal end_btn, int Direction)
        {
            if (Direction == 0)
            {
                ptr.next_s = begin_btn; ptr.next_w = null; ptr.next_t = null;
                while (ptr.next_w != null || ptr.next_s != null || ptr.next_t != null)
                {
                    //判断指针是否指向轨道区段类
                    if (ptr.next_t != null)
                    {
                        //锁闭轨道区段
                        ptr.next_t.locked = false; ptr.next_t.LJ_1 = true; ptr.next_t.LJ_2 = true;
                        //进行下一节点类型判断
                        if (ptr.next_t.next_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_t.next_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }
                        if (ptr.next_t.next_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_t.next_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }
                    }
                    //判断指针是否指向道岔类
                    if (ptr.next_w != null)
                    {
                        //道岔恢复至定位（没有单锁的情况下）
                        if (ptr.next_w.locked == false)
                        {
                            ptr.next_w.DBJ = true; ptr.next_w.FBJ = false;
                        }
                        //确保双动道岔一致变换
                        DoubleSwitchChange();
                        //锁闭道岔
                        ptr.next_w.SJ = true;
                        //进行下一节点类型判断
                        if (ptr.next_w.next_t != null)  //判断下一节点是否为轨道区段类
                        {
                            ptr.next_t = ptr.next_w.next_t;
                            continue;
                        }

                        if (ptr.next_w.next_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_w.next_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }

                        if (ptr.next_w.next_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_w.next_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }

                    }
                    //判断指针是否指向信号机类
                    if (ptr.next_s != null)
                    {
                        //开放信号显示
                        ptr.next_s.XJ = false;
                        if (ptr.next_s == end_btn) //若指针搜索到了终端按钮，则进路建立完成
                        {
                            ptr.next_s.next_t.locked = false; ptr.next_s.next_t.LJ_1 = true; ptr.next_s.next_t.LJ_2 = true;
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + begin_btn.name + "至" + end_btn.name + "进路已成功取消！", Color.Green);
                            break;
                        }
                        else
                        {
                            //进行下一节点类型判断
                            if (ptr.next_s.next_t != null)  //判断下一节点是否为轨道区段类
                            {
                                ptr.next_t = ptr.next_s.next_t;
                                ptr.next_s = null;
                                ptr.next_w = null;
                                continue;
                            }
                            if (ptr.next_s.next_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_s.next_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }
                    }
                }
                //更新站场显示
                UpdateSignal(); UpdateSwitch(); UpdateTrack();
                //开放模拟行车按钮功能
                Btn_MNXC.Enabled = false;

            }
            else if (Direction == 1)
            {
                ptr.next_s = begin_btn; ptr.next_w = null; ptr.next_t = null;
                while (ptr.next_w != null || ptr.next_s != null || ptr.next_t != null)
                {
                    //判断指针是否指向轨道区段类
                    if (ptr.next_t != null)
                    {
                        //锁闭轨道区段
                        ptr.next_t.locked = false; ptr.next_t.LJ_1 = true; ptr.next_t.LJ_2 = true;
                        //进行下一节点类型判断
                        if (ptr.next_t.pre_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_t.pre_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }
                        if (ptr.next_t.pre_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_t.pre_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }
                    }
                    //判断指针是否指向道岔类
                    if (ptr.next_w != null)
                    {
                        //道岔恢复至定位（没有单锁的情况下）
                        if(ptr.next_w.locked == false)
                        {
                            ptr.next_w.DBJ = true; ptr.next_w.FBJ = false;
                        }
                        //确保双动道岔一致变换
                        DoubleSwitchChange();
                        //锁闭道岔
                        ptr.next_w.SJ = true;
                        //进行下一节点类型判断
                        if (ptr.next_w.pre_t != null)  //判断下一节点是否为轨道区段类
                        {
                            ptr.next_t = ptr.next_w.pre_t;
                            continue;
                        }

                        if (ptr.next_w.pre_s != null)  //判断下一节点是否为信号机类
                        {
                            ptr.next_s = ptr.next_w.pre_s;
                            ptr.next_t = null;
                            ptr.next_w = null;
                            continue;
                        }

                        if (ptr.next_w.pre_w != null)  //判断下一节点是否为道岔类
                        {
                            ptr.next_w = ptr.next_w.pre_w;
                            ptr.next_s = null;
                            ptr.next_t = null;
                            continue;
                        }

                    }
                    //判断指针是否指向信号机类
                    if (ptr.next_s != null)
                    {
                        //开放信号显示
                        ptr.next_s.XJ = false;
                        if (ptr.next_s == end_btn) //若指针搜索到了终端按钮，则进路建立完成
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + begin_btn.name + "至" + end_btn.name + "进路已成功取消！", Color.Green);
                            break;
                        }
                        else
                        {
                            //进行下一节点类型判断
                            if (ptr.next_s.pre_t != null)  //判断下一节点是否为轨道区段类
                            {
                                ptr.next_t = ptr.next_s.pre_t;
                                ptr.next_s = null;
                                ptr.next_w = null;
                                continue;
                            }
                            if (ptr.next_s.pre_w != null)  //判断下一节点是否为道岔类
                            {
                                ptr.next_w = ptr.next_s.pre_w;
                                ptr.next_s = null;
                                ptr.next_t = null;
                                continue;
                            }
                        }
                    }
                }
                //更新站场显示
                UpdateSignal(); UpdateSwitch(); UpdateTrack();
                //开放模拟行车按钮功能
                Btn_MNXC.Enabled = false;
            }
        }
        //根据信号机状态进行图形显示
        public void UpdateSignal()
        {
            //列车信号机
            //S
            if ((S.DJ == false || S.XJ == true) && (type[0] == 2 || type[0] == 3 || type[1] == 2 || type[1] == 3 || type[2] == 2 || type[2] == 3 || type[3] == 2 || type[3] == 3 || type[4] == 2 || type[4] == 3 || type[5] == 2 || type[5] == 3)) //引导或引导总锁
            {
                if (DS_timer.Enabled)
                {
                    DS_timer.Enabled = false;
                }
                S_1.FillColor = Color.Red;
                S_2.FillColor = Color.White;
            }
            else if (S.DJ == false)  //灯丝断丝
            {
                DS_timer.Enabled = true;
                S_2.FillColor = Color.Black;
            }
            else if(S.XJ == false)  //信号未开放
            {
                if(DS_timer.Enabled)
                {
                    DS_timer.Enabled = false;
                }
                S_1.FillColor = Color.Red;
                S_2.FillColor = Color.Black;
            }
            else if(X_II.XJ == true && type[0] == 0)  //正线接车至IIG
            {
                S_1.FillColor = Color.Yellow;
                S_2.FillColor = Color.Black;
            }
            else if((X_3.XJ == true && type[2] == 0)||(X_I.XJ == true && type[3] == 0) ||(X_4.XJ == true && type[4] == 0) ||(X_6.XJ == true && type[5] == 0)||(X_8.XJ == true && type[1] == 0))  //侧线接车
            {
                S_1.FillColor = Color.Yellow;
                S_2.FillColor = Color.Yellow;
            }

            //SF

            //X3
            if (X_3.XJ == false)  //信号未开放
            {
                X3_1.FillColor = Color.Red;
                X3_2.FillColor = Color.Black;
            }
            else if (type[2] == 1)
            {
                X3_1.FillColor = Color.Green;
            }
            //XI
            if (X_I.XJ == false)  //信号未开放
            {
                XI_1.FillColor = Color.Red;
                XI_2.FillColor = Color.Black;
            }
            else if (type[3] == 1)
            {
                XI_1.FillColor = Color.Green;
            }
            //XII
            if (X_II.XJ == false)  //信号未开放
            {
                XII_1.FillColor = Color.Red;
                XII_2.FillColor = Color.Black;
            }
            else if(type[0] == 1)
            {
                XII_1.FillColor = Color.Green;
            }
            //X4
            if (X_4.XJ == false)  //信号未开放
            {
                X4_1.FillColor = Color.Red;
                X4_2.FillColor = Color.Black;
            }
            else if (type[4] == 1)
            {
                X4_1.FillColor = Color.Green;
            }
            //X6
            if (X_6.XJ == false)  //信号未开放
            {
                X6_1.FillColor = Color.Red;
                X6_2.FillColor = Color.Black;
            }
            else if (type[5] == 1)
            {
                X6_1.FillColor = Color.Green;
            }
            //X8
            if (X_8.XJ == false)  //信号未开放
            {
                X8_1.FillColor = Color.Red;
                X8_2.FillColor = Color.Black;
            }
            else if (type[1] == 1)
            {
                X8_1.FillColor = Color.Green;
            }
            //调车信号机
            //D2

            //D4

            //D6
            if (XD_6.XJ == false)  //信号未开放
            {
                D6.FillColor = Color.Blue;
            }
            else if (type[6] == 5)
            {
                D6.FillColor = Color.White;
            }
            //D8
            if (XD_8.XJ == false)  //信号未开放
            {
                D8.FillColor = Color.Blue;
            }
            else if (type[6] == 6)
            {
                D8.FillColor = Color.White;
            }
            
        }
        //根据轨道区段状态进行图形显示
        public void UpdateTrack()
        {
        //S接近
            //1. 故障
            if(S_approach.error == 1 || S_approach.state == 2)
            {
                JJ.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if(S_approach.locked == true && S_approach.LJ_1 == false && S_approach.LJ_2 == false && S_approach.state == 0)
            {
                JJ.BorderColor = Color.White;
            }
            //3. 占用或未完全解锁过程中
            else if(S_approach.state == 1 || (S_approach.locked == true && (S_approach.LJ_1 == true || S_approach.LJ_2 == true)))
            {
                JJ.BorderColor= Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                JJ.BorderColor = Color.FromArgb(128, 128, 255);
            }

        //SF接近
            //1. 故障
            if (SF_approach.error == 1 || SF_approach.state == 2)
            {
                JJF.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (SF_approach.locked == true && SF_approach.LJ_1 == false && SF_approach.LJ_2 == false && SF_approach.state == 0)
            {
                JJF.BorderColor = Color.White;
            }
            //3. 占用或未完全解锁过程中
            else if (SF_approach.state == 1 || (SF_approach.locked == true && (SF_approach.LJ_1 == true || SF_approach.LJ_2 == true)))
            {
                JJF.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                JJF.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //IBG
            //1. 故障
            if (DG_IBG.error == 1 || DG_IBG.state == 2)
            {
                IBG.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_IBG.locked == true && DG_IBG.LJ_1 == false && DG_IBG.LJ_2 == false && DG_IBG.state == 0)
            {
                IBG.BorderColor = Color.White;
            }
            //3. 占用或未完全解锁过程中
            else if (DG_IBG.state == 1 || (DG_IBG.locked == true && (DG_IBG.LJ_1 == true || DG_IBG.LJ_2 == true)))
            {
                IBG.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                IBG.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //2DG
            //1. 故障
            if (DG_2.error == 1 || DG_2.state == 2)
            {
                if(D_2.DBJ == true && D_2.FBJ == false)
                {
                    DG2_1.BorderColor = Color.Red;
                    DG2_4.BorderColor = Color.Red;
                }
                else if(D_2.DBJ == false && D_2.FBJ == true)
                {
                    DG2_1.BorderColor = Color.Red;
                    DG2_5.BorderColor = Color.Red;
                }
            }
            //2. 锁闭（进路建立后）
            else if (DG_2.locked == true && DG_2.LJ_1 == false && DG_2.LJ_2 == false && DG_2.state == 0)
            {
                
            }
            //3. 占用或未完全解锁过程中
            else if (DG_2.state == 1 || (DG_2.locked == true && (DG_2.LJ_1 == true || DG_2.LJ_2 == true)))
            {
                
            }
            //4. 解锁或空闲
            else
            {
                DG2_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG2_4.BorderColor = Color.FromArgb(128, 128, 255);
                DG2_5.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //4DG
            //1. 故障
            if (DG_4.error == 1 || DG_4.state == 2)
            {
                if (D_4.DBJ == true && D_4.FBJ == false)
                {
                    DG4_1.BorderColor = Color.Red;
                    DG4_4.BorderColor = Color.Red;
                }
                else if (D_4.DBJ == false && D_4.FBJ == true)
                {
                    DG4_1.BorderColor = Color.Red;
                    DG4_5.BorderColor = Color.Red;
                }
            }
            //2. 锁闭（进路建立后）
            else if (DG_4.locked == true && DG_4.LJ_1 == false && DG_4.LJ_2 == false && DG_4.state == 0)
            {
                //S<->XII/X8/X3/XI/X4/X6
                if((type[0] != 3 && type[0] != 4 && road_exist[0] == true)||(type[1] != 3 && type[1] != 4 && road_exist[1] == true) || (type[2] != 3 && type[2] != 4 && road_exist[2] == true) || (type[3] != 3 && type[3] != 4 && road_exist[3] == true) || (type[4] != 3 && type[4] != 4 && road_exist[4] == true) || (type[5] != 3 && type[5] != 4 && road_exist[5] == true))
                {
                    DG4_1.BorderColor = Color.White;
                    DG4_4.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_4.state == 1 || (DG_4.locked == true && (DG_4.LJ_1 == true || DG_4.LJ_2 == true)))
            {
                //S<->XII/X8/X3/XI/X4/X6
                if ((type[0] != 3 && type[0] != 4 && road_exist[0] == true) || (type[1] != 3 && type[1] != 4 && road_exist[1] == true) || (type[2] != 3 && type[2] != 4 && road_exist[2] == true) || (type[3] != 3 && type[3] != 4 && road_exist[3] == true) || (type[4] != 3 && type[4] != 4 && road_exist[4] == true) || (type[5] != 3 && type[5] != 4 && road_exist[5] == true))
                {
                    DG4_1.BorderColor = Color.Red;
                    DG4_4.BorderColor = Color.Red;
                }
            }
            //4. 解锁或空闲
            else
            {
                DG4_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG4_4.BorderColor = Color.FromArgb(128, 128, 255);
                DG4_5.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //8DG
            //1. 故障
            if (DG_8.error == 1 || DG_8.state == 2)
            {
                if (D_8.DBJ == true && D_8.FBJ == false)
                {
                    DG8_1.BorderColor = Color.Red;
                    DG8_4.BorderColor = Color.Red;
                }
                else if (D_8.DBJ == false && D_8.FBJ == true)
                {
                    DG8_1.BorderColor = Color.Red;
                    DG8_5.BorderColor = Color.Red;
                }
            }
            //2. 锁闭（进路建立后）
            else if (DG_8.locked == true && DG_8.LJ_1 == false && DG_8.LJ_2 == false && DG_8.state == 0)
            {
                //S<->X3/XI D6<->D8
                if ((type[2] != 3 && type[2] != 4 && road_exist[2] == true)||(type[3] != 3 && type[3] != 4 && road_exist[3] == true)||(type[6] != 3 && type[6] != 4 && road_exist[6] == true))
                {
                    DG8_1.BorderColor = Color.White;
                    DG8_5.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_8.state == 1 || (DG_8.locked == true && (DG_8.LJ_1 == true || DG_8.LJ_2 == true)))
            {
                //S<->X3/XI
                if ((type[2] != 3 && type[2] != 4 && road_exist[2] == true) || (type[3] != 3 && type[3] != 4 && road_exist[3] == true))
                {
                    DG8_1.BorderColor = Color.Red;
                    DG8_5.BorderColor = Color.Red;
                }
            }
            //4. 解锁或空闲
            else
            {
                DG8_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG8_4.BorderColor = Color.FromArgb(128, 128, 255);
                DG8_5.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //6-14DG
            //1. 故障
            if (DG_6_14.error == 1 || DG_6_14.state == 2)
            {
                if (D_6.DBJ == false && D_6.FBJ == true)
                {
                    DG6_1.BorderColor = Color.Red;
                    DG6_5.BorderColor = Color.Red;
                }
                else if (D_6.DBJ == true && D_6.FBJ == false && D_10.DBJ == true && D_10.FBJ == false)
                {
                    DG6_1.BorderColor = Color.Red;
                    DG10_1.BorderColor = Color.Red;
                    DG10_4.BorderColor = Color.Red;
                }
                else if(D_6.DBJ == true && D_6.FBJ == false && D_10.DBJ == false && D_10.FBJ == true && D_14.DBJ == true && D_14.FBJ == false)
                {
                    DG6_1.BorderColor = Color.Red;
                    DG10_1.BorderColor = Color.Red;
                    DG14_1.BorderColor = Color.Red;
                    DG14_41.BorderColor = Color.Red;
                    DG14_42.BorderColor = Color.Red;
                }
                else if (D_6.DBJ == true && D_6.FBJ == false && D_10.DBJ == false && D_10.FBJ == true && D_14.DBJ == false && D_14.FBJ == true)
                {
                    DG6_1.BorderColor = Color.Red;
                    DG10_1.BorderColor = Color.Red;
                    DG14_1.BorderColor = Color.Red;
                    DG14_51.BorderColor = Color.Red;
                    DG14_52.BorderColor = Color.Red;
                }
            }
            //2. 锁闭（进路建立后）
            else if (DG_6_14.locked == true && DG_6_14.LJ_1 == false && DG_6_14.LJ_2 == false && DG_6_14.state == 0)
            {
                //S<->XII/X4
                if ((type[0] != 3 && type[0] != 4 && road_exist[0] == true)||(type[4] != 3 && type[4] != 4 && road_exist[4] == true))
                {
                    DG6_1.BorderColor = Color.White;
                    DG10_1.BorderColor = Color.White;
                    DG10_4.BorderColor = Color.White;
                }
                //S<->X8
                if (type[1] != 3 && type[1] != 4 && road_exist[1] == true)
                {
                    DG6_1.BorderColor = Color.White;
                    DG10_1.BorderColor = Color.White;
                    DG14_1.BorderColor = Color.White;
                    DG14_51.BorderColor = Color.White;
                    DG14_52.BorderColor = Color.White;
                }
                //S<->X3/XI D6<->D8
                if ((type[2] != 3 && type[2] != 4 && road_exist[2] == true)||(type[3] != 3 && type[3] != 4 && road_exist[3] == true) || (type[6] != 3 && type[6] != 4 && road_exist[6] == true))
                {
                    DG6_1.BorderColor = Color.White;
                    DG6_5.BorderColor = Color.White;
                }
                //S<->X6
                if (type[5] != 3 && type[5] != 4 && road_exist[5] == true)
                {
                    DG6_1.BorderColor = Color.White;
                    DG10_1.BorderColor = Color.White;
                    DG14_1.BorderColor = Color.White;
                    DG14_41.BorderColor = Color.White;
                    DG14_42.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_6_14.state == 1 || (DG_6_14.locked == true && (DG_6_14.LJ_1 == true || DG_6_14.LJ_2 == true)))
            {
                //S<->XII/X4
                if ((type[0] != 3 && type[0] != 4 && road_exist[0] == true) || (type[4] != 3 && type[4] != 4 && road_exist[4] == true))
                {
                    DG6_1.BorderColor = Color.Red;
                    DG10_1.BorderColor = Color.Red;
                    DG10_4.BorderColor = Color.Red;
                }
                //S<->X8
                if (type[1] != 3 && type[1] != 4 && road_exist[1] == true)
                {
                    DG6_1.BorderColor = Color.Red;
                    DG10_1.BorderColor = Color.Red;
                    DG14_1.BorderColor = Color.Red;
                    DG14_51.BorderColor = Color.Red;
                    DG14_52.BorderColor = Color.Red;
                }
                //S<->X3/XI
                if ((type[2] != 3 && type[2] != 4 && road_exist[2] == true) || (type[3] != 3 && type[3] != 4 && road_exist[3] == true))
                {
                    DG6_1.BorderColor = Color.Red;
                    DG6_5.BorderColor = Color.Red;
                }
                //S<->X6
                if (type[5] != 3 && type[5] != 4 && road_exist[5] == true)
                {
                    DG6_1.BorderColor = Color.Red;
                    DG10_1.BorderColor = Color.Red;
                    DG14_1.BorderColor = Color.Red;
                    DG14_41.BorderColor = Color.Red;
                    DG14_42.BorderColor = Color.Red;
                }
            }
            //4. 解锁或空闲
            else
            {
                DG6_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG6_5.BorderColor = Color.FromArgb(128, 128, 255);
                DG10_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG10_4.BorderColor = Color.FromArgb(128, 128, 255);
                DG14_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG14_41.BorderColor = Color.FromArgb(128, 128, 255);
                DG14_42.BorderColor = Color.FromArgb(128, 128, 255);
                DG14_51.BorderColor = Color.FromArgb(128, 128, 255);
                DG14_52.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //12DG
            //1. 故障
            if (DG_12.error == 1 || DG_12.state == 2)
            {
                if (D_12.DBJ == true && D_12.FBJ == false)
                {
                    DG12_1.BorderColor = Color.Red;
                    DG12_4.BorderColor = Color.Red;
                }
                else if (D_12.DBJ == false && D_12.FBJ == true)
                {
                    DG12_1.BorderColor = Color.Red;
                    DG12_51.BorderColor = Color.Red;
                    DG12_52.BorderColor = Color.Red;
                }
            }
            //2. 锁闭（进路建立后）
            else if (DG_12.locked == true && DG_12.LJ_1 == false && DG_12.LJ_2 == false && DG_12.state == 0)
            {
                //S<->X3
                if (type[2] != 3 && type[2] != 4 && road_exist[2] == true)
                {
                    DG12_1.BorderColor = Color.White;
                    DG12_51.BorderColor = Color.White;
                    DG12_52.BorderColor = Color.White;
                }
                //S<->XI
                if (type[3] != 3 && type[3] != 4 && road_exist[3] == true)
                {
                    DG12_1.BorderColor = Color.White;
                    DG12_4.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_12.state == 1 || (DG_12.locked == true && (DG_12.LJ_1 == true || DG_12.LJ_2 == true)))
            {
                //S<->X3
                if (type[2] != 3 && type[2] != 4 && road_exist[2] == true)
                {
                    DG12_1.BorderColor = Color.Red;
                    DG12_51.BorderColor = Color.Red;
                    DG12_52.BorderColor = Color.Red;
                }
                //S<->XI
                if (type[3] != 3 && type[3] != 4 && road_exist[3] == true)
                {
                    DG12_1.BorderColor = Color.Red;
                    DG12_4.BorderColor = Color.Red;
                }
            }
            //4. 解锁或空闲
            else
            {
                DG12_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG12_4.BorderColor = Color.FromArgb(128, 128, 255);
                DG12_51.BorderColor = Color.FromArgb(128, 128, 255);
                DG12_52.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //16DG
            //1. 故障
            if (DG_16.error == 1 || DG_16.state == 2)
            {
                if (D_16.DBJ == true && D_16.FBJ == false)
                {
                    DG16_1.BorderColor = Color.Red;
                    DG16_4.BorderColor = Color.Red;
                }
                else if (D_16.DBJ == false && D_16.FBJ == true)
                {
                    DG16_1.BorderColor = Color.Red;
                    DG16_51.BorderColor = Color.Red;
                    DG16_52.BorderColor = Color.Red;
                }
            }
            //2. 锁闭（进路建立后）
            else if (DG_16.locked == true && DG_16.LJ_1 == false && DG_16.LJ_2 == false && DG_16.state == 0)
            {
                //S<->XII
                if (type[0] != 3 && type[0] != 4 && road_exist[0] == true)
                {
                    DG16_1.BorderColor = Color.White;
                    DG16_4.BorderColor = Color.White;
                }
                //S<->X4
                if (type[4] != 3 && type[4] != 4 && road_exist[4] == true)
                {
                    DG16_1.BorderColor = Color.White;
                    DG16_51.BorderColor = Color.White;
                    DG16_52.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_16.state == 1 || (DG_16.locked == true && (DG_16.LJ_1 == true || DG_16.LJ_2 == true)))
            {
                //S<->XII
                if (type[0] != 3 && type[0] != 4 && road_exist[0] == true)
                {
                    DG16_1.BorderColor = Color.Red;
                    DG16_4.BorderColor = Color.Red;
                }
                //S<->X4
                if (type[4] != 3 && type[4] != 4 && road_exist[4] == true)
                {
                    DG16_1.BorderColor = Color.Red;
                    DG16_51.BorderColor = Color.Red;
                    DG16_52.BorderColor = Color.Red;
                }
            }
            //4. 解锁或空闲
            else
            {
                DG16_1.BorderColor = Color.FromArgb(128, 128, 255);
                DG16_4.BorderColor = Color.FromArgb(128, 128, 255);
                DG16_51.BorderColor = Color.FromArgb(128, 128, 255);
                DG16_52.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //3G
            //1. 故障
            if (DG_3G.error == 1 || DG_3G.state == 2)
            {
                G3G.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_3G.locked == true && DG_3G.LJ_1 == false && DG_3G.LJ_2 == false && DG_3G.state == 0)
            {
                if (type[2] != 3)
                {
                    G3G.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_3G.state == 1 || (DG_3G.locked == true && (DG_3G.LJ_1 == true || DG_3G.LJ_2 == true)))
            {
                G3G.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                G3G.BorderColor = Color.FromArgb(128, 128, 255);
            }
        //IG
            //1. 故障
            if (DG_IG.error == 1 || DG_IG.state == 2)
            {
                GIG.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_IG.locked == true && DG_IG.LJ_1 == false && DG_IG.LJ_2 == false && DG_IG.state == 0)
            {
                if(type[3] != 3)
                {
                    GIG.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_IG.state == 1 || (DG_IG.locked == true && (DG_IG.LJ_1 == true || DG_IG.LJ_2 == true)))
            {
                GIG.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                GIG.BorderColor = Color.FromArgb(128, 128, 255);
            }
         //IIG
            //1. 故障
            if (DG_IIG.error == 1 || DG_IIG.state == 2)
            {
                GIIG.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_IIG.locked == true && DG_IIG.LJ_1 == false && DG_IIG.LJ_2 == false && DG_IIG.state == 0)
            {
                if (type[0] != 3)
                {
                    GIIG.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_IIG.state == 1 || (DG_IIG.locked == true && (DG_IIG.LJ_1 == true || DG_IIG.LJ_2 == true)))
            {
                GIIG.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                GIIG.BorderColor = Color.FromArgb(128, 128, 255);
            }
         //4G
            //1. 故障
            if (DG_4G.error == 1 || DG_4G.state == 2)
            {
                G4G.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_4G.locked == true && DG_4G.LJ_1 == false && DG_4G.LJ_2 == false && DG_4G.state == 0)
            {
                if (type[4] != 3)
                {
                    G4G.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_4G.state == 1 || (DG_4G.locked == true && (DG_4G.LJ_1 == true || DG_4G.LJ_2 == true)))
            {
                G4G.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                G4G.BorderColor = Color.FromArgb(128, 128, 255);
            }
         //6G
            //1. 故障
            if (DG_6G.error == 1 || DG_6G.state == 2)
            {
                G6G.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_6G.locked == true && DG_6G.LJ_1 == false && DG_6G.LJ_2 == false && DG_6G.state == 0)
            {
                if (type[5] != 3)
                {
                    G6G.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_6G.state == 1 || (DG_6G.locked == true && (DG_6G.LJ_1 == true || DG_6G.LJ_2 == true)))
            {
                G6G.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                G6G.BorderColor = Color.FromArgb(128, 128, 255);
            }
         //8G
            //1. 占用或故障
            if (DG_8G.error == 1 || DG_8G.state == 2)
            {
                G8G.BorderColor = Color.Red;
            }
            //2. 锁闭（进路建立后）
            else if (DG_8G.locked == true && DG_8G.LJ_1 == false && DG_8G.LJ_2 == false && DG_8G.state == 0)
            {
                if (type[1] != 3)
                {
                    G8G.BorderColor = Color.White;
                }
            }
            //3. 占用或未完全解锁过程中
            else if (DG_8G.state == 1 || (DG_8G.locked == true && (DG_8G.LJ_1 == true || DG_8G.LJ_2 == true)))
            {
                G8G.BorderColor = Color.Red;
            }
            //4. 解锁或空闲
            else
            {
                G8G.BorderColor = Color.FromArgb(128, 128, 255);
            }
        }
        //根据道岔状态进行图形显示
        public void UpdateSwitch()
        {
         //D_2 D_4
            //1. 单锁显示
            if (D_2.locked == true && D_4.locked == true)
            {
                DS_2.Visible = true; DS_4.Visible = true;
            }
            //2. 定反位显示（涵盖故障恢复）
            if ((D_2.DBJ == true && D_2.FBJ == false)&&(D_4.DBJ == true && D_4.FBJ == false)&&(D_2.error == 0 && D_4.error == 0))
            {
                DG2_2.Visible = true;
                DG2_3.Visible = false;
                DG2_2.BorderColor = Color.Lime;
                L2.ForeColor = Color.Lime;
                DG4_2.Visible = true;
                DG4_3.Visible = false;
                DG4_2.BorderColor = Color.Lime;
                L4.ForeColor = Color.Lime;
            }
            if((D_2.DBJ == false && D_2.FBJ == true)&&(D_4.DBJ == false && D_4.FBJ == true) && (D_2.error == 0 && D_4.error == 0))
            {
                DG2_2.Visible = false;
                DG2_3.Visible = true;
                DG2_3.BorderColor = Color.Yellow;
                L2.ForeColor = Color.Yellow;
                DG4_2.Visible = false;
                DG4_3.Visible = true;
                DG4_3.BorderColor = Color.Yellow;
                L4.ForeColor = Color.Yellow;
            }
            
            //3. 单解显示
            if (D_2.locked == false && D_4.locked == false)
            {
                DS_2.Visible = false; DS_4.Visible = false;
            }
            //4. 四开显示
            if(D_2.error == 1 && D_4.error == 1)
            {
                DG2_2.BorderColor = Color.Red;
                DG2_3.BorderColor = Color.Red;
                L2.ForeColor = Color.Red;
                DG2_2.Visible = true;
                DG2_3.Visible = true;
                DG4_2.BorderColor = Color.Red;
                DG4_3.BorderColor = Color.Red;
                L4.ForeColor = Color.Red;
                DG4_2.Visible = true;
                DG4_3.Visible = true;
                D_2_4_timer.Enabled = true;
            }
            //5. 失表显示
            if(D_2.error == 2 && D_4.error == 2)
            {
                DG2_2.Visible = false; DG2_3.Visible = false;
                DG4_2.Visible = false; DG4_3.Visible = false;
                L2.ForeColor = Color.Red; L4.ForeColor = Color.Red;
            }
            //6. 道岔封锁显示
            if(D_2.blocked == true && D_4.blocked == true)
            {
                F2.Visible = true; F4.Visible = true;
            }
            //7. 道岔解封显示
            if (D_2.blocked == false && D_4.blocked == false)
            {
                F2.Visible = false; F4.Visible = false;
            }
        //D_6 D_8
            //1. 单锁显示
            if (D_6.locked == true && D_8.locked == true)
            {
                DS_6.Visible = true; DS_8.Visible = true;
            }
            //2. 定反位显示（涵盖故障恢复）
            if ((D_6.DBJ == true && D_6.FBJ == false)&&(D_8.DBJ == true && D_8.FBJ == false) && (D_6.error == 0 && D_8.error == 0))
            {
                DG6_2.Visible = true;
                DG6_3.Visible = false;
                DG6_2.BorderColor = Color.Lime;
                L6.ForeColor = Color.Lime;
                DG8_2.Visible = true;
                DG8_3.Visible = false;
                DG8_2.BorderColor = Color.Lime;
                L8.ForeColor = Color.Lime;
            }
            if ((D_6.DBJ == false && D_6.FBJ == true)&&(D_8.DBJ == false && D_8.FBJ == true) && (D_6.error == 0 && D_8.error == 0))
            {
                DG6_2.Visible = false;
                DG6_3.Visible = true;
                DG6_3.BorderColor = Color.Yellow;
                L6.ForeColor = Color.Yellow;
                DG8_2.Visible = false;
                DG8_3.Visible = true;
                DG8_3.BorderColor = Color.Yellow;
                L8.ForeColor = Color.Yellow;
            }
            
            //3. 单解显示
            if (D_6.locked == false && D_8.locked == false)
            {
                DS_6.Visible = false; DS_8.Visible = false;
            }
            //4. 四开显示
            if (D_6.error == 1 && D_8.error == 1)
            {
                DG6_2.BorderColor = Color.Red;
                DG6_3.BorderColor = Color.Red;
                L6.ForeColor = Color.Red;
                DG6_2.Visible = true;
                DG6_3.Visible = true;
                DG8_2.BorderColor = Color.Red;
                DG8_3.BorderColor = Color.Red;
                L8.ForeColor = Color.Red;
                DG8_2.Visible = true;
                DG8_3.Visible = true;
                D_6_8_timer.Enabled = true;
            }
            //5. 失表显示
            if (D_6.error == 2 && D_8.error == 2)
            {
                DG6_2.Visible = false; DG6_3.Visible = false;
                DG8_2.Visible = false; DG8_3.Visible = false;
                L6.ForeColor = Color.Red; L8.ForeColor = Color.Red;
            }
            //6. 道岔封锁显示
            if (D_6.blocked == true && D_8.blocked == true)
            {
                F6.Visible = true; F8.Visible = true;
            }
            //7. 道岔解封显示
            if (D_6.blocked == false && D_8.blocked == false)
            {
                F6.Visible = false; F8.Visible = false;
            }
        //D_10
            //1. 单锁显示
            if (D_10.locked == true)
            {
                DS_10.Visible = true;
            }
            //2. 定反位显示（涵盖故障恢复）
            if (D_10.DBJ == true && D_10.FBJ == false && D_10.error == 0)
            {
                DG10_2.Visible = true;
                DG10_3.Visible = false;
                DG10_2.BorderColor = Color.Lime;
                L10.ForeColor = Color.Lime;
            }
            if (D_10.DBJ == false && D_10.FBJ == true && D_10.error == 0)
            {
                DG10_2.Visible = false;
                DG10_3.Visible = true;
                DG10_3.BorderColor = Color.Yellow;
                L10.ForeColor = Color.Yellow;
            }
            
            //3. 单解显示
            if (D_10.locked == false)
            {
                DS_10.Visible = false;
            }
            //4. 四开显示
            if (D_10.error == 1)
            {
                DG10_2.BorderColor = Color.Red;
                DG10_3.BorderColor = Color.Red;
                L10.ForeColor = Color.Red;
                DG10_2.Visible = true;
                DG10_3.Visible = true;
                D_10_timer.Enabled = true;
            }
            //5. 失表显示
            if (D_10.error == 2)
            {
                DG10_2.Visible = false; DG10_3.Visible = false;
                L10.ForeColor = Color.Red;
            }
            //6. 道岔封锁显示
            if (D_10.blocked == true)
            {
                F10.Visible = true;
            }
            //7. 道岔解封显示
            if (D_10.blocked == false)
            {
                F10.Visible = false;
            }
        //D_12
            //1. 单锁显示
            if (D_12.locked == true)
            {
                DS_12.Visible = true;
            }
            //2. 定反位显示（涵盖故障恢复）
            if (D_12.DBJ == true && D_12.FBJ == false && D_12.error == 0)
            {
                DG12_2.Visible = true;
                DG12_3.Visible = false;
                DG12_2.BorderColor = Color.Lime;
                L12.ForeColor = Color.Lime;
            }
            if (D_12.DBJ == false && D_12.FBJ == true && D_12.error == 0)
            {
                DG12_2.Visible = false;
                DG12_3.Visible = true;
                DG12_3.BorderColor = Color.Yellow;
                L12.ForeColor = Color.Yellow;
            }
            
            //3. 单解显示
            if (D_12.locked == false)
            {
                DS_12.Visible = false;
            }
            //4. 四开显示
            if (D_12.error == 1)
            {
                DG12_2.BorderColor = Color.Red;
                DG12_3.BorderColor = Color.Red;
                L12.ForeColor = Color.Red;
                DG12_2.Visible = true;
                DG12_3.Visible = true;
                D_12_timer.Enabled = true;
            }
            //5. 失表显示
            if (D_12.error == 2)
            {
                DG12_2.Visible = false; DG12_3.Visible = false;
                L12.ForeColor = Color.Red;
            }
            //6. 道岔封锁显示
            if (D_12.blocked == true)
            {
                F12.Visible = true;
            }
            //7. 道岔解封显示
            if (D_12.blocked == false)
            {
                F12.Visible = false;
            }
         //D_14
            //1. 单锁显示
            if (D_14.locked == true)
            {
                DS_14.Visible = true;
            }
            //2. 定反位显示（涵盖故障恢复）
            if (D_14.DBJ == true && D_14.FBJ == false && D_14.error == 0)
            {
                DG14_2.Visible = true;
                DG14_3.Visible = false;
                DG14_2.BorderColor = Color.Lime;
                L14.ForeColor = Color.Lime;
            }
            if (D_14.DBJ == false && D_14.FBJ == true && D_14.error == 0)
            {
                DG14_2.Visible = false;
                DG14_3.Visible = true;
                DG14_3.BorderColor = Color.Yellow;
                L14.ForeColor = Color.Yellow;
            }
            
            //3. 单解显示
            if (D_14.locked == false)
            {
                DS_14.Visible = false;
            }
            //4. 四开显示
            if (D_14.error == 1)
            {
                DG14_2.BorderColor = Color.Red;
                DG14_3.BorderColor = Color.Red;
                L14.ForeColor = Color.Red;
                DG14_2.Visible = true;
                DG14_3.Visible = true;
                D_14_timer.Enabled = true;
            }
            //5. 失表显示
            if (D_14.error == 2)
            {
                DG14_2.Visible = false; DG14_3.Visible = false;
                L14.ForeColor = Color.Red;
            }
            //6. 道岔封锁显示
            if (D_14.blocked == true)
            {
                F14.Visible = true;
            }
            //7. 道岔解封显示
            if (D_14.blocked == false)
            {
                F14.Visible = false;
            }
        //D_16
            //1. 单锁显示
            if (D_16.locked == true)
            {
                DS_16.Visible = true;
            }
            //2. 定反位显示（涵盖故障恢复）
            if (D_16.DBJ == true && D_16.FBJ == false && D_16.error == 0)
            {
                DG16_2.Visible = true;
                DG16_3.Visible = false;
                DG16_2.BorderColor = Color.Lime;
                L16.ForeColor = Color.Lime;
            }
            if (D_16.DBJ == false && D_16.FBJ == true && D_16.error == 0)
            {
                DG16_2.Visible = false;
                DG16_3.Visible = true;
                DG16_3.BorderColor = Color.Yellow;
                L16.ForeColor = Color.Yellow;
            }
            
            //3. 单解显示
            if (D_16.locked == false)
            {
                DS_16.Visible = false;
            }
            //4. 四开显示
            if (D_16.error == 1)
            {
                DG16_2.BorderColor = Color.Red;
                DG16_3.BorderColor = Color.Red;
                L16.ForeColor = Color.Red;
                DG16_2.Visible = true;
                DG16_3.Visible = true;
                D_16_timer.Enabled = true;
            }
            //5. 失表显示
            if (D_16.error == 2)
            {
                DG16_2.Visible = false; DG16_3.Visible = false;
                L16.ForeColor = Color.Red;
            }
            //6. 道岔封锁显示
            if (D_16.blocked == true)
            {
                F16.Visible = true;
            }
            //7. 道岔解封显示
            if (D_16.blocked == false)
            {
                F16.Visible = false;
            }
        }
        //确保双动道岔转换一致性
        public void DoubleSwitchChange()
        {
            if (D_4.DBJ == true && D_4.FBJ == false)
            {
                D_2.DBJ = true; D_2.FBJ = false;
            }
            if (D_4.DBJ == false && D_4.FBJ == true)
            {
                D_2.DBJ = false; D_2.FBJ = true;
            }
            if (D_6.DBJ == true && D_6.FBJ == false)
            {
                D_8.DBJ = true; D_8.FBJ = false;
            }
            if (D_6.DBJ == false && D_6.FBJ == true)
            {
                D_8.DBJ = false; D_8.FBJ = true;
            }
        }
        //初始化各设备状态
        private void FrmMain_Load(object sender, EventArgs e)
        {

            // 1.信号机
            S.name = "信号机S"; S.pressed = false; S.XJ = false; S.DJ = true; S.approach_seg = S_approach;
            S_F.name = "信号机SF"; S_F.pressed = false; S_F.XJ = false; S_F.DJ = true; S_F.approach_seg = SF_approach;
            X_3.name = "信号机X3"; X_3.pressed = false; X_3.XJ = false; X_3.DJ = true; X_3.approach_seg = DG_3G;
            X_I.name = "信号机XI"; X_I.pressed = false; X_I.XJ = false; X_I.DJ = true; X_I.approach_seg = DG_IG;
            X_II.name = "信号机XII"; X_II.pressed = false; X_II.XJ = false; X_II.DJ = true; X_II.approach_seg = DG_IIG;
            X_4.name = "信号机X4"; X_4.pressed = false; X_4.XJ = false; X_4.DJ = true; X_4.approach_seg = DG_4G;
            X_6.name = "信号机X6"; X_6.pressed = false; X_6.XJ = false; X_6.DJ = true; X_6.approach_seg = DG_6G;
            X_8.name = "信号机X8"; X_8.pressed = false; X_8.XJ = false; X_8.DJ = true; X_8.approach_seg = DG_8G;
            XD_2.name = "信号机D2"; XD_2.pressed = false; XD_2.XJ = false; XD_2.DJ = true; XD_2.approach_seg = DG_IBG;
            XD_4.name = "信号机D4"; XD_4.pressed = false; XD_4.XJ = false; XD_4.DJ = true; XD_4.approach_seg = DG_8;
            XD_6.name = "信号机D6"; XD_6.pressed = false; XD_6.XJ = false; XD_6.DJ = true; XD_6.approach_seg = DG_4;
            XD_8.name = "信号机D8"; XD_8.pressed = false; XD_8.XJ = false; XD_8.DJ = true; XD_8.approach_seg = DG_8;
            // 2.轨道区段
            S_approach.name = "S接近区段"; S_approach.state = 0; S_approach.error = 0; S_approach.locked = false; S_approach.LJ_1 = true; S_approach.LJ_2 = true;
            SF_approach.name = "SF接近区段"; SF_approach.state = 0; SF_approach.error = 0; SF_approach.locked = false; SF_approach.LJ_1 = true; SF_approach.LJ_2 = true;
            DG_IBG.name = "轨道区段IBG"; DG_IBG.state = 0; DG_IBG.error = 0; DG_IBG.locked = false; DG_IBG.LJ_1 = true; DG_IBG.LJ_2 = true;
            DG_2.name = "轨道区段2DG"; DG_2.state = 0; DG_2.error = 0; DG_2.locked = false; DG_2.LJ_1 = true; DG_2.LJ_2 = true;
            DG_4.name = "轨道区段4DG"; DG_4.state = 0; DG_4.error = 0; DG_4.locked = false; DG_4.LJ_1 = true; DG_4.LJ_2 = true;
            DG_8.name = "轨道区段8DG"; DG_8.state = 0; DG_8.error = 0; DG_8.locked = false; DG_8.LJ_1 = true; DG_8.LJ_2 = true;
            DG_6_14.name = "轨道区段6-14DG"; DG_6_14.state = 0; DG_6_14.error = 0; DG_6_14.locked = false; DG_6_14.LJ_1 = true; DG_6_14.LJ_2 = true;
            DG_12.name = "轨道区段12DG"; DG_12.state = 0; DG_12.error = 0; DG_12.locked = false; DG_12.LJ_1 = true; DG_12.LJ_2 = true;
            DG_16.name = "轨道区段16DG"; DG_16.state = 0; DG_16.error = 0; DG_16.locked = false; DG_16.LJ_1 = true; DG_16.LJ_2 = true;
            DG_3G.name = "轨道区段3G"; DG_3G.state = 0; DG_3G.error = 0; DG_3G.locked = false; DG_3G.LJ_1 = true; DG_3G.LJ_2 = true;
            DG_IG.name = "轨道区段IG"; DG_IG.state = 0; DG_IG.error = 0; DG_IG.locked = false; DG_IG.LJ_1 = true; DG_IG.LJ_2 = true;
            DG_IIG.name = "轨道区段IIG"; DG_IIG.state = 0; DG_IIG.error = 0; DG_IIG.locked = false; DG_IIG.LJ_1 = true; DG_IIG.LJ_2 = true;
            DG_4G.name = "轨道区段4G"; DG_4G.state = 0; DG_4G.error = 0; DG_4G.locked = false; DG_4G.LJ_1 = true; DG_4G.LJ_2 = true;
            DG_6G.name = "轨道区段6G"; DG_6G.state = 0; DG_6G.error = 0; DG_6G.locked = false; DG_6G.LJ_1 = true; DG_6G.LJ_2 = true;
            DG_8G.name = "轨道区段8G"; DG_8G.state = 0; DG_8G.error = 0; DG_8G.locked = false; DG_8G.LJ_1 = true; DG_8G.LJ_2 = true;
            // 3.道岔
            D_2.name = "2/4号道岔"; D_2.state = 0; D_2.error = 0; D_2.DBJ = true; D_2.FBJ = false; D_2.locked = false; D_2.SJ = true; D_2.blocked = false; D_2.seg = DG_2;
            D_4.name = "2/4号道岔"; D_4.state = 0; D_4.error = 0; D_4.DBJ = true; D_4.FBJ = false; D_4.locked = false; D_4.SJ = true; D_4.blocked = false; D_4.seg = DG_4;
            D_6.name = "6/8号道岔"; D_6.state = 0; D_6.error = 0; D_6.DBJ = true; D_6.FBJ = false; D_6.locked = false; D_6.SJ = true; D_6.blocked = false; D_6.seg = DG_6_14;
            D_8.name = "6/8号道岔"; D_8.state = 0; D_8.error = 0; D_8.DBJ = true; D_8.FBJ = false; D_8.locked = false; D_8.SJ = true; D_8.blocked = false; D_8.seg = DG_8;
            D_10.name = "10号道岔"; D_10.state = 0; D_10.error = 0; D_10.DBJ = true; D_10.FBJ = false; D_10.locked = false; D_10.SJ = true; D_10.blocked = false; D_10.seg = DG_6_14;
            D_12.name = "12号道岔"; D_12.state = 0; D_12.error = 0; D_12.DBJ = true; D_12.FBJ = false; D_12.locked = false; D_12.SJ = true; D_12.blocked = false; D_12.seg = DG_12;
            D_14.name = "14号道岔"; D_14.state = 0; D_14.error = 0; D_14.DBJ = true; D_14.FBJ = false; D_14.locked = false; D_14.SJ = true; D_14.blocked = false; D_14.seg = DG_6_14;
            D_16.name = "16号道岔"; D_16.state = 0; D_16.error = 0; D_16.DBJ = true; D_16.FBJ = false; D_16.locked = false; D_16.SJ = true; D_16.blocked = false; D_16.seg = DG_16;
            Btn_MNXC.Enabled = false;
        }

        //进路始终端按钮检测，判断进路，总取消总人解功能体现
        private void SLA_Click(object sender, EventArgs e)
        {
            if(Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                SLA.BackColor = Color.Yellow;
                S.pressed = true;
                //判断按下按钮对应哪条进路
                //XII->S
                if (X_II.pressed == true)
                {
                    BuildDataList_S_XII();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(X_II, S, 1) == 1)
                    {
                        type[0] = 1; road_exist[0] = true;
                        BuildRoad(X_II, S, 1);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    S.pressed = false; X_II.pressed = false;
                    SLA.BackColor = Color.Green;
                    XIILA.BackColor = Color.Green;
                }
                //X_8->S
                if (X_8.pressed == true)
                {
                    BuildDataList_S_X8();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(X_8, S, 1) == 1)
                    {
                        type[1] = 1; road_exist[1] = true;
                        BuildRoad(X_8, S, 1);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    S.pressed = false; X_8.pressed = false;
                    SLA.BackColor = Color.Green;
                    X8LA.BackColor = Color.Green;
                }
                //X3->S
                if (X_3.pressed == true)
                {
                    BuildDataList_S_X3();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(X_3, S, 1) == 1)
                    {
                        type[2] = 1; road_exist[2] = true;
                        BuildRoad(X_3, S, 1);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    S.pressed = false; X_3.pressed = false;
                    SLA.BackColor = Color.Green;
                    X3LA.BackColor = Color.Green;
                }
                //XI->S
                if (X_I.pressed == true)
                {
                    BuildDataList_S_XI();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(X_I, S, 1) == 1)
                    {
                        type[3] = 1; road_exist[3] = true;
                        BuildRoad(X_I, S, 1);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    S.pressed = false; X_I.pressed = false;
                    SLA.BackColor = Color.Green;
                    XILA.BackColor = Color.Green;
                }
                //X4->S
                if (X_4.pressed == true)
                {
                    BuildDataList_S_X4();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(X_4, S, 1) == 1)
                    {
                        type[4] = 1; road_exist[4] = true;
                        BuildRoad(X_4, S, 1);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    S.pressed = false; X_4.pressed = false;
                    SLA.BackColor = Color.Green;
                    X4LA.BackColor = Color.Green;
                }
                //X6->S
                if (X_6.pressed == true)
                {
                    BuildDataList_S_X6();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(X_6, S, 1) == 1)
                    {
                        type[5] = 1; road_exist[5] = true;
                        BuildRoad(X_6, S, 1);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    S.pressed = false; X_6.pressed = false;
                    SLA.BackColor = Color.Green;
                    X6LA.BackColor = Color.Green;
                }
            }
            else  if(Btn_ZQ.BackColor == Color.Yellow)  //S总取消
            {
                //S->X_II取消进路
                if (road_exist[0] && type[0] == 0)  
                {
                    if (S.approach_seg.state == 2 || S.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(S, X_II, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[0] = false;
                        type[0] = 4;
                    }
                }
                else if(road_exist[0] && type[0] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_8取消进路
                if (road_exist[1] && type[1] == 0)  
                {
                    if (S.approach_seg.state == 2 || S.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(S, X_8, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[1] = false;
                        type[1] = 4;
                    }
                }
                else if(road_exist[1] && type[1] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_3取消进路
                if (road_exist[2] && type[2] == 0)
                {
                    if (S.approach_seg.state == 2 || S.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(S, X_3, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[2] = false;
                        type[2] = 4;
                    }
                }
                else if (road_exist[2] && type[2] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_I取消进路
                if (road_exist[3] && type[3] == 0)
                {
                    if (S.approach_seg.state == 2 || S.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(S, X_I, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[3] = false;
                        type[3] = 4;
                    }
                }
                else if (road_exist[3] && type[3] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_4取消进路
                if (road_exist[4] && type[4] == 0)
                {
                    if (S.approach_seg.state == 2 || S.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(S, X_4, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[4] = false;
                        type[4] = 4;
                    }
                }
                else if (road_exist[4] && type[4] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_6取消进路
                if (road_exist[5] && type[5] == 0)
                {
                    if (S.approach_seg.state == 2 || S.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(S, X_6, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[5] = false;
                        type[5] = 4;
                    }
                }
                else if (road_exist[5] && type[5] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if(Btn_ZRJ.BackColor == Color.Yellow)  //S总人解
            {
                //S->X_II人工解锁
                if (road_exist[0] && (type[0] == 0 || type[0] == 2 || type[0] == 3))  
                {
                    if (S.approach_seg.state == 0 && S_2.FillColor != Color.White)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_II.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        if(type[0] == 2 || type[0] == 3)
                        {
                            CancelRoad(S, X_II, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                            road_exist[0] = false;
                            type[0] = 4;
                        }
                        else
                        {
                            //人工延时解锁操作
                            timer_180.Enabled = true;
                        }
                    }
                }
                else if (road_exist[0] && type[0] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_8人工解锁
                if (road_exist[1] && (type[1] == 0 || type[1] == 2 || type[1] == 3))  
                {
                    if (S.approach_seg.state == 0 && S_2.FillColor != Color.White)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_8.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        if (type[1] == 2 || type[1] == 3)
                        {
                            CancelRoad(S, X_8, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                            road_exist[1] = false;
                            type[1] = 4;
                        }
                        else
                        {
                            //人工延时解锁操作
                            timer_180.Enabled = true;
                        }
                    }
                }
                else if (road_exist[1] && type[1] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_3人工解锁
                if (road_exist[2] && (type[2] == 0 || type[2] == 2 || type[2] == 3))
                {
                    if (S.approach_seg.state == 0 && S_2.FillColor != Color.White)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_3.XJ = false; XD_6.XJ = false; XD_8.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        if (type[2] == 2 || type[2] == 3)
                        {
                            CancelRoad(S, X_3, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                            road_exist[2] = false;
                            type[2] = 4;
                        }
                        else
                        {
                            //人工延时解锁操作
                            timer_180.Enabled = true;
                        }
                    }
                }
                else if (road_exist[2] && type[2] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_I人工解锁
                if (road_exist[3] && (type[3] == 0 || type[3] == 2 || type[3] == 3))
                {
                    if (S.approach_seg.state == 0 && S_2.FillColor != Color.White)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_I.XJ = false; XD_6.XJ = false; XD_8.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        if (type[3] == 2 || type[3] == 3)
                        {
                            CancelRoad(S, X_I, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                            road_exist[3] = false;
                            type[3] = 4;
                        }
                        else
                        {
                            //人工延时解锁操作
                            timer_180.Enabled = true;
                        }
                    }
                }
                else if (road_exist[3] && type[3] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_4人工解锁
                if (road_exist[4] && (type[4] == 0 || type[4] == 2 || type[4] == 3))
                {
                    if (S.approach_seg.state == 0 && S_2.FillColor != Color.White)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_4.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        if (type[4] == 2 || type[4] == 3)
                        {
                            CancelRoad(S, X_4, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                            road_exist[4] = false;
                            type[4] = 4;
                        }
                        else
                        {
                            //人工延时解锁操作
                            timer_180.Enabled = true;
                        }
                    }
                }
                else if (road_exist[4] && type[4] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
                //S->X_6人工解锁
                if (road_exist[5] && (type[5] == 0 || type[5] == 2 || type[5] == 3))
                {
                    if (S.approach_seg.state == 0 && S_2.FillColor != Color.White)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_6.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        if (type[5] == 2 || type[5] == 3)
                        {
                            CancelRoad(S, X_6, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                            road_exist[5] = false;
                            type[5] = 4;
                        }
                        else
                        {
                            //人工延时解锁操作
                            timer_180.Enabled = true;
                        }
                    }
                }
                else if (road_exist[5] && type[5] == 1)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void XIILA_Click(object sender, EventArgs e)
        {
            if(Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                XIILA.BackColor = Color.Yellow;
                X_II.pressed = true;
                //判断按下按钮对应哪条进路
                //S->XII
                if (S.pressed == true)
                {
                    BuildDataList_S_XII();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(S, X_II, 0) == 1)
                    {
                        type[0] = 0; road_exist[0] = true;
                        BuildRoad(S, X_II, 0);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    X_II.pressed = false;
                    S.pressed = false;
                    SLA.BackColor = Color.Green;
                    XIILA.BackColor = Color.Green;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //X_II总取消
            {
                //X_II->S取消进路
                if (road_exist[0] && type[0] == 1)  
                {
                    if (X_II.approach_seg.state == 2 || X_II.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(X_II, S, 1);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[0] = false;
                        type[0] = 4;
                    }
                }
                else if (road_exist[0] && type[0] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //XII总人解
            {
                //X_II->S人工解锁
                if (road_exist[0] && type[0] == 1)  
                {
                    if (X_II.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_II.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_180.Enabled = true;
                        
                    }
                }
                else if (road_exist[0] && type[0] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }

        }
        private void X8LA_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                X8LA.BackColor = Color.Yellow;
                X_8.pressed = true;
                //判断按下按钮对应哪条进路
                //S->X8
                if (S.pressed == true)
                {
                    BuildDataList_S_X8();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(S, X_8, 0) == 1 && DG_16.state == 0)
                    {
                        type[1] = 0; road_exist[1] = true;
                        BuildRoad(S, X_8, 0);
                    }
                    else if(DG_16.state == 2 ||DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    X_8.pressed = false;
                    S.pressed = false;
                    SLA.BackColor = Color.Green;
                    X8LA.BackColor = Color.Green;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //X_8总取消
            {
                //X_8->S取消进路
                if (road_exist[1] && type[1] == 1)  
                {
                    if (X_8.approach_seg.state == 2 || X_8.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(X_8, S, 1);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[1] = false;
                        type[1] = 4;
                    }
                }
                else if (road_exist[1] && type[1] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //X8总人解
            {
                //X_8->S人工解锁
                if (road_exist[1] && type[1] == 1)  
                {
                    if (X_8.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_8.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_30.Enabled = true;
                        
                    }
                }
                else if (road_exist[1] && type[1] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void X3LA_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                X3LA.BackColor = Color.Yellow;
                X_3.pressed = true;
                //判断按下按钮对应哪条进路
                //S->X3
                if (S.pressed == true)
                {
                    BuildDataList_S_X3();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(S, X_3, 0) == 1)
                    {
                        type[2] = 0; road_exist[2] = true;
                        BuildRoad(S, X_3, 0);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    X_3.pressed = false;
                    S.pressed = false;
                    SLA.BackColor = Color.Green;
                    X3LA.BackColor = Color.Green;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //X_3总取消
            {
                //X_3->S取消进路
                if (road_exist[2] && type[2] == 1)
                {
                    if (X_3.approach_seg.state == 2 || X_3.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(X_3, S, 1);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[2] = false;
                        type[2] = 4;
                    }
                }
                else if (road_exist[2] && type[2] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //X3总人解
            {
                //X_3->S人工解锁
                if (road_exist[2] && type[2] == 1)
                {
                    if (X_3.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_3.XJ = false; XD_6.XJ = false; XD_8.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_30.Enabled = true;

                    }
                }
                else if (road_exist[2] && type[2] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void XILA_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                XILA.BackColor = Color.Yellow;
                X_I.pressed = true;
                //判断按下按钮对应哪条进路
                //S->XI
                if (S.pressed == true)
                {
                    BuildDataList_S_XI();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(S, X_I, 0) == 1)
                    {
                        type[3] = 0; road_exist[3] = true;
                        BuildRoad(S, X_I, 0);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    X_I.pressed = false;
                    S.pressed = false;
                    SLA.BackColor = Color.Green;
                    XILA.BackColor = Color.Green;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //X_I总取消
            {
                //X_I->S取消进路
                if (road_exist[3] && type[3] == 1)
                {
                    if (X_I.approach_seg.state == 2 || X_I.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(X_I, S, 1);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[3] = false;
                        type[3] = 4;
                    }
                }
                else if (road_exist[3] && type[3] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //XI总人解
            {
                //X_I->S人工解锁
                if (road_exist[3] && type[3] == 1)
                {
                    if (X_I.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_I.XJ = false; XD_6.XJ = false; XD_8.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_30.Enabled = true;
                    }
                }
                else if (road_exist[3] && type[3] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void X4LA_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                X4LA.BackColor = Color.Yellow;
                X_4.pressed = true;
                //判断按下按钮对应哪条进路
                //S->X4
                if (S.pressed == true)
                {
                    BuildDataList_S_X4();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(S, X_4, 0) == 1)
                    {
                        type[4] = 0; road_exist[4] = true;
                        BuildRoad(S, X_4, 0);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    X_4.pressed = false;
                    S.pressed = false;
                    SLA.BackColor = Color.Green;
                    X4LA.BackColor = Color.Green;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //X_4总取消
            {
                //X_4->S取消进路
                if (road_exist[4] && type[4] == 1)
                {
                    if (X_4.approach_seg.state == 2 || X_4.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(X_4, S, 1);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[4] = false;
                        type[4] = 4;
                    }
                }
                else if (road_exist[4] && type[4] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //X4总人解
            {
                //X_4->S人工解锁
                if (road_exist[4] && type[4] == 1)
                {
                    if (X_4.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_4.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_30.Enabled = true;
                    }
                }
                else if (road_exist[4] && type[4] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void X6LA_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                X6LA.BackColor = Color.Yellow;
                X_6.pressed = true;
                //判断按下按钮对应哪条进路
                //S->X6
                if (S.pressed == true)
                {
                    BuildDataList_S_X6();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(S, X_6, 0) == 1 && DG_16.state == 0)
                    {
                        type[5] = 0; road_exist[5] = true;
                        BuildRoad(S, X_6, 0);
                    }
                    else if (DG_16.state == 2 || DG_16.error == 1)
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    X_6.pressed = false;
                    S.pressed = false;
                    SLA.BackColor = Color.Green;
                    X6LA.BackColor = Color.Green;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //X_6总取消
            {
                //X_6->S取消进路
                if (road_exist[5] && type[5] == 1)
                {
                    if (X_6.approach_seg.state == 2 || X_6.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(X_6, S, 1);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[5] = false;
                        type[5] = 4;
                    }
                }
                else if (road_exist[5] && type[5] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //X6总人解
            {
                //X_6->S人工解锁
                if (road_exist[5] && type[5] == 1)
                {
                    if (X_6.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        S.XJ = false; X_6.XJ = false; XD_6.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_30.Enabled = true;

                    }
                }
                else if (road_exist[5] && type[5] == 0)  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void D6_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                XD_6.pressed = true;
                DC_timer.Enabled = true;
                //判断按下按钮对应哪条进路
                //D8->D6
                if (XD_8.pressed == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "该进路不存在！", Color.Red);
                    //恢复进路按钮
                    LD8.Visible = true;
                    XD_8.pressed = false;
                    XD_6.pressed = false;
                }
            }
            else if (Btn_ZQ.BackColor == Color.Yellow)  //D6总取消
            {
                //D6->D8取消进路
                if (road_exist[6] && type[6] == 5)
                {
                    if (XD_6.approach_seg.state == 2 || XD_6.approach_seg.error == 1)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段占用，请办理人工解锁！", Color.Red);
                    }
                    else
                    {
                        CancelRoad(XD_6, XD_8, 0);
                        Btn_ZQ.BackColor = Color.White;
                        //取消进路记录
                        road_exist[6] = false;
                        type[6] = 4;
                    }
                }
                else if (road_exist[6])  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
            else if (Btn_ZRJ.BackColor == Color.Yellow)  //X6总人解
            {
                //X_6->S人工解锁
                if (road_exist[6] && type[6] == 5)
                {
                    if (XD_6.approach_seg.state == 0)  //检查接近区段是否无车
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "接近区段未占用，请办理总取消进路！", Color.Red);
                    }
                    else
                    {
                        //关闭信号
                        XD_6.XJ = false; XD_8.XJ = false;
                        UpdateSignal();
                        //恢复总人解按钮
                        Btn_ZRJ.BackColor = Color.White;
                        //取消模拟行车按钮功能
                        Btn_MNXC.Enabled = false;
                        //人工延时解锁操作
                        timer_30.Enabled = true;

                    }
                }
                else if (road_exist[6])  //按错按钮
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "错误操作，请点击进路始端按钮！", Color.Red);
                }
            }
        }
        private void D8_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White && Btn_ZRJ.BackColor == Color.White)  //进路办理
            {
                XD_8.pressed = true;
                DC_timer.Enabled = true;
                //判断按下按钮对应哪条进路
                //D6->D8
                if (XD_6.pressed == true)
                {
                    BuildDataList_D6_D8();
                    //联锁逻辑检查是否通过
                    if (InterloackingCheck(XD_6, XD_8, 0) == 1)
                    {
                        type[6] = 5; road_exist[6] = true;
                        BuildRoad(XD_6, XD_8, 0);
                    }
                    else
                    {
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "联锁检查未通过！", Color.Red);
                    }
                    //恢复进路按钮
                    LD6.Visible = true;
                    XD_8.pressed = false;
                    XD_6.pressed = false;
                }
            }
        }
        //办理S引导进路/引导总锁闭进路
        private void SYDA_Click(object sender, EventArgs e)
        {
            Dialogue dlg = new Dialogue();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                SYDA.BackColor = Color.Yellow;
            }
            else
            {
                MessageBox.Show("口令输入不正确！");
            }
            if(SYDA.BackColor == Color.Yellow)
            {
                //引导进路
                if(Btn_YDZS.BackColor == Color.White)
                {
                    //引导至IIG
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.DBJ == true && D_16.DBJ == true)  
                    {
                        //检查有无敌对信号
                        if (S.XJ == true || X_II.XJ == true || XD_6.XJ == true)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "存在敌对信号，无法办理！", Color.Red);
                        }
                        else
                        {
                            type[0] = 2; road_exist[0] = true;
                            BuildDataList_S_XII();
                            BuildRoad(S, X_II, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_II.name + "的引导进路！", Color.Green);
                        }
                    }
                    //引导至8G
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.FBJ == true && D_14.FBJ == true)  
                    {
                        //检查有无敌对信号
                        if (S.XJ == true || X_8.XJ == true || XD_6.XJ == true)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "存在敌对信号，无法办理！", Color.Red);
                        }
                        else if(DG_16.state == 2 || DG_16.error == 1)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                        }
                        else
                        {
                            type[1] = 2; road_exist[1] = true;
                            BuildDataList_S_X8();
                            BuildRoad(S, X_8, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_8.name + "的引导进路！", Color.Green);
                        }
                    }
                    //引导至3G
                    if (D_4.DBJ == true && D_6.FBJ == true && D_12.FBJ == true)
                    {
                        //检查有无敌对信号
                        if (S.XJ == true || X_3.XJ == true || XD_6.XJ == true || XD_8.XJ == true)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "存在敌对信号，无法办理！", Color.Red);
                        }
                        else
                        {
                            type[2] = 2; road_exist[2] = true;
                            BuildDataList_S_X3();
                            BuildRoad(S, X_3, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_3.name + "的引导进路！", Color.Green);
                        }
                    }
                    //引导至IG
                    if (D_4.DBJ == true && D_6.FBJ == true && D_12.DBJ == true)
                    {
                        //检查有无敌对信号
                        if (S.XJ == true || X_I.XJ == true || XD_6.XJ == true || XD_8.XJ == true)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "存在敌对信号，无法办理！", Color.Red);
                        }
                        else
                        {
                            type[3] = 2; road_exist[3] = true;
                            BuildDataList_S_XI();
                            BuildRoad(S, X_I, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_I.name + "的引导进路！", Color.Green);
                        }
                    }
                    //引导至4G
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.DBJ == true && D_16.FBJ == true)
                    {
                        //检查有无敌对信号
                        if (S.XJ == true || X_4.XJ == true || XD_6.XJ == true)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "存在敌对信号，无法办理！", Color.Red);
                        }
                        else
                        {
                            type[4] = 2; road_exist[4] = true;
                            BuildDataList_S_X4();
                            BuildRoad(S, X_4, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_4.name + "的引导进路！", Color.Green);
                        }
                    }
                    //引导至6G
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.FBJ == true && D_14.DBJ == true)
                    {
                        //检查有无敌对信号
                        if (S.XJ == true || X_6.XJ == true || XD_6.XJ == true)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "存在敌对信号，无法办理！", Color.Red);
                        }
                        else if (DG_16.state == 2 || DG_16.error == 1)
                        {
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "不满足侵限绝缘条件！", Color.Red);
                        }
                        else
                        {
                            type[5] = 2; road_exist[5] = true;
                            BuildDataList_S_X6();
                            BuildRoad(S, X_6, 0);
                            OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_6.name + "的引导进路！", Color.Green);
                        }
                    }
                }
                //引导总锁闭进路
                if (Btn_YDZS.BackColor == Color.Yellow)
                {
                    //引导至IIG
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.DBJ == true && D_16.DBJ == true)  
                    {
                        type[0] = 3; road_exist[0] = true;
                        //begin = S; end = X_II;
                        //开放信号显示
                        S.XJ = true; X_II.XJ = true; XD_6.XJ = true;
                        UpdateSignal();
                        //开放模拟行车按钮功能
                        Btn_MNXC.Enabled = true;
                        BuildDataList_S_XII();
                        BuildRoad(S, X_II, 0);
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_II.name + "的引导总锁闭进路！", Color.Green);

                    }
                    //引导至8G
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.FBJ == true && D_14.FBJ == true)  
                    {
                        type[1] = 3;road_exist[1] = true;
                        //begin = S; end = X_8;
                        //开放信号显示
                        S.XJ = true; X_8.XJ = true; XD_6.XJ = true;
                        UpdateSignal();
                        //开放模拟行车按钮功能
                        Btn_MNXC.Enabled = true;
                        BuildDataList_S_X8();
                        BuildRoad(S, X_8, 0);
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_8.name + "的引导总锁闭进路！", Color.Green);
                    }
                    //引导至3G
                    if (D_4.DBJ == true && D_6.FBJ == true && D_12.FBJ == true)
                    {
                        type[2] = 3; road_exist[2] = true;
                        //begin = S; end = X_II;
                        //开放信号显示
                        S.XJ = true; X_3.XJ = true; XD_6.XJ = true; XD_8.XJ = true;
                        UpdateSignal();
                        //开放模拟行车按钮功能
                        Btn_MNXC.Enabled = true;
                        BuildDataList_S_X3();
                        BuildRoad(S, X_3, 0);
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_3.name + "的引导总锁闭进路！", Color.Green);
                    }
                    //引导至IG
                    if (D_4.DBJ == true && D_6.FBJ == true && D_12.DBJ == true)
                    {
                        type[3] = 3; road_exist[3] = true;
                        //begin = S; end = X_II;
                        //开放信号显示
                        S.XJ = true; X_I.XJ = true; XD_6.XJ = true; XD_8.XJ = true;
                        UpdateSignal();
                        //开放模拟行车按钮功能
                        Btn_MNXC.Enabled = true;
                        BuildDataList_S_XI();
                        BuildRoad(S, X_I, 0);
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_I.name + "的引导总锁闭进路！", Color.Green);
                    }
                    //引导至4G
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.DBJ == true && D_16.FBJ == true)
                    {
                        type[4] = 3; road_exist[4] = true;
                        //begin = S; end = X_II;
                        //开放信号显示
                        S.XJ = true; X_4.XJ = true; XD_6.XJ = true;
                        UpdateSignal();
                        //开放模拟行车按钮功能
                        Btn_MNXC.Enabled = true;
                        BuildDataList_S_X4();
                        BuildRoad(S, X_4, 0);
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_4.name + "的引导总锁闭进路！", Color.Green);
                    }
                    //引导至6G
                    if (D_4.DBJ == true && D_6.DBJ == true && D_10.FBJ == true && D_14.DBJ == true)
                    {
                        type[5] = 3; road_exist[5] = true;
                        //begin = S; end = X_8;
                        //开放信号显示
                        S.XJ = true; X_6.XJ = true; XD_6.XJ = true;
                        UpdateSignal();
                        //开放模拟行车按钮功能
                        Btn_MNXC.Enabled = true;
                        BuildDataList_S_X6();
                        BuildRoad(S, X_6, 0);
                        OutMsg(PromptWindow, DateTime.Now.ToString() + "：已办理" + S.name + "至" + X_6.name + "的引导总锁闭进路！", Color.Green);
                    }
                }
                //恢复引导按钮
                SYDA.BackColor = Color.DarkBlue;
            }
            
        }

        //按钮功能实现
        //总定位按钮
        private void Btn_ZD_Click(object sender, EventArgs e)
        {
            if(Btn_ZD.BackColor == Color.White)
            {
                Btn_ZD.BackColor = Color.Yellow;
            }
            else
            {
                Btn_ZD.BackColor = Color.White;
            }
        }
        //总反位按钮
        private void Btn_ZF_Click(object sender, EventArgs e)
        {
            if (Btn_ZF.BackColor == Color.White)
            {
                Btn_ZF.BackColor = Color.Yellow;
            }
            else
            {
                Btn_ZF.BackColor = Color.White;
            }
        }
        //道岔单锁按钮
        private void Btn_DCDS_Click(object sender, EventArgs e)
        {
            if (Btn_DCDS.BackColor == Color.White)
            {
                Btn_DCDS.BackColor = Color.Yellow;
            }
            else
            {
                Btn_DCDS.BackColor = Color.White;
            }
        }
        //道岔单解按钮
        private void Btn_DCDJ_Click(object sender, EventArgs e)
        {
            if (Btn_DCDJ.BackColor == Color.White)
            {
                Btn_DCDJ.BackColor = Color.Yellow;
            }
            else
            {
                Btn_DCDJ.BackColor = Color.White;
            }
        }
        //总取消按钮
        private void Btn_ZQ_Click(object sender, EventArgs e)
        {
            if (Btn_ZQ.BackColor == Color.White)
            {
                Btn_ZQ.BackColor = Color.Yellow;
            }
            else
            {
                Btn_ZQ.BackColor = Color.White;
            }
        }
        //总人解按钮
        private void Btn_ZRJ_Click(object sender, EventArgs e)
        {
            Dialogue dlg = new Dialogue();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                Btn_ZRJ.BackColor = Color.Yellow;
            }
            else
            {
                MessageBox.Show("口令输入不正确！");
            }

        }
        //道岔封锁按钮
        private void Btn_DCFS_Click(object sender, EventArgs e)
        {
            if (Btn_DCFS.BackColor == Color.White)
            {
                Btn_DCFS.BackColor = Color.Yellow;
            }
            else
            {
                Btn_DCFS.BackColor = Color.White;
            }
        }
        //道岔解封按钮
        private void Btn_DCJF_Click(object sender, EventArgs e)
        {
            if (Btn_DCJF.BackColor == Color.White)
            {
                Btn_DCJF.BackColor = Color.Yellow;
            }
            else
            {
                Btn_DCJF.BackColor = Color.White;
            }
        }
        //S引导总锁按钮
        private void Btn_YDZS_Click(object sender, EventArgs e)
        {
            if(Btn_YDZS.BackColor == Color.White)
            {
                Dialogue dlg = new Dialogue();
                dlg.ShowDialog();
                if (dlg.DialogResult == DialogResult.OK)
                {
                    Btn_YDZS.BackColor = Color.Yellow;
                    //咽喉所有道岔全部锁闭
                    D_2.SJ = false; D_4.SJ = false;
                    D_6.SJ = false; D_8.SJ = false;
                    D_10.SJ = false; D_12.SJ = false;
                    D_14.SJ = false; D_16.SJ = false;
                }
                else
                {
                    MessageBox.Show("口令输入不正确！");
                }
            }
            else
            {
                Dialogue dlg = new Dialogue();
                dlg.ShowDialog();
                if (dlg.DialogResult == DialogResult.OK)
                {
                    Btn_YDZS.BackColor = Color.White;
                }
                else
                {
                    MessageBox.Show("口令输入不正确！");
                }
            }
        }
        //灯丝断丝按钮
        private void Btn_DSDS_Click(object sender, EventArgs e)
        {
            if (Btn_DSDS.BackColor == Color.White)
            {
                Btn_DSDS.BackColor = Color.Yellow;
            }
            else
            {
                Btn_DSDS.BackColor = Color.White;
            }
        }
        //断丝恢复按钮
        private void Btn_DSHF_Click(object sender, EventArgs e)
        {
            if (Btn_DSHF.BackColor == Color.White)
            {
                Btn_DSHF.BackColor = Color.Yellow;
            }
            else
            {
                Btn_DSHF.BackColor = Color.White;
            }
        }
        //模拟行车按钮
        private void Btn_MNXC_Click(object sender, EventArgs e)
        {
            if(Btn_MNXC.BackColor == Color.White)
            {
                Btn_MNXC.BackColor = Color.Yellow;
            }
            else
            {
                Btn_MNXC.BackColor = Color.White;
            }
            //根据进路判断模拟行车路线
            if(Btn_MNXC.BackColor == Color.Yellow)  //按下模拟行车按钮
            {
                //S->XII接车进路
                if (road_exist[0] && type[0] == 0)  
                {
                    Train_S.Location = new Point(1445, 350);
                    Train_S.Visible = true;
                    timer_S_XII.Enabled = true;
                }
                
            }
            else
            {
                Train_S.Visible = false;
            }
        }

        //道岔操作实现
        private void L2_Click(object sender, EventArgs e)
        {
            //2/4号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if(D_2.blocked == true && D_4.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if(D_2.error == 1 && D_4.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "处于四开状态，无法操作！", Color.Red);
                }
                //道岔未单锁、未锁闭时才能操作
                else if(D_2.locked == false && D_4.locked == false && D_2.SJ == true && D_4.SJ == true)
                {
                    //更改道岔状态
                    D_2.DBJ = false; D_2.FBJ = true;
                    D_4.DBJ = false; D_4.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //2/4号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_2.blocked == true && D_4.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_2.error == 1 && D_4.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_2.locked == false && D_4.locked == false && D_2.SJ == true && D_4.SJ == true)
                {
                    //更改道岔状态
                    D_2.DBJ = true; D_2.FBJ = false;
                    D_4.DBJ = true; D_4.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //2/4号道岔单锁操作
            if(Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_2.locked = true;
                D_4.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已单锁！", Color.Red);
            }
            //2/4号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_2.locked = false;
                D_4.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已单解！", Color.Green);
            }
            //2/4号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_2.blocked = true; D_4.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已封锁！", Color.Red);
            }
            //2/4号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_2.blocked = false; D_4.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "已解封！", Color.Green);
            }
        }
        private void L4_Click(object sender, EventArgs e)
        {
            //2/4号道岔总反位操作
            if(Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_2.blocked == true && D_4.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_2.error == 1 && D_4.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_2.locked == false && D_4.locked == false && D_2.SJ == true && D_4.SJ == true)
                {
                    //更改道岔状态
                    D_2.DBJ = false; D_2.FBJ = true;
                    D_4.DBJ = false; D_4.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //2/4号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_2.blocked == true && D_4.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_2.error == 1 && D_4.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_2.locked == false && D_4.locked == false && D_2.SJ == true && D_4.SJ == true)
                {
                    //更改道岔状态
                    D_2.DBJ = true; D_2.FBJ = false;
                    D_4.DBJ = true; D_4.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //2/4号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_2.locked = true;
                D_4.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已单锁！", Color.Red);
            }
            //2/4号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_2.locked = false;
                D_4.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已单解！", Color.Green);
            }
            //2/4号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_2.blocked = true; D_4.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已封锁！", Color.Red);
            }
            //2/4号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_2.blocked = false; D_4.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "已解封！", Color.Green);
            }
        }
        private void L6_Click(object sender, EventArgs e)
        {
            //6/8号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_6.blocked == true && D_8.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_6.error == 1 && D_8.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_6.locked == false && D_8.locked == false && D_6.SJ == true && D_8.SJ == true)
                {
                    //更改道岔状态
                    D_6.DBJ = false; D_6.FBJ = true;
                    D_8.DBJ = false; D_8.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //6/8号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_6.blocked == true && D_8.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_6.error == 1 && D_8.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_6.locked == false && D_8.locked == false && D_6.SJ == true && D_8.SJ == true)
                {
                    //更改道岔状态
                    D_6.DBJ = true; D_6.FBJ = false;
                    D_8.DBJ = true; D_8.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //6/8号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_6.locked = true;
                D_8.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已单锁！", Color.Red);
            }
            //6/8号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_6.locked = false;
                D_8.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已单解！", Color.Green);
            }
            //6/8号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_6.blocked = true; D_8.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已封锁！", Color.Red);
            }
            //6/8号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_6.blocked = false; D_8.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "已解封！", Color.Green);
            }
        }
        private void L8_Click(object sender, EventArgs e)
        {
            //6/8号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_6.blocked == true && D_8.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_6.error == 1 && D_8.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_6.locked == false && D_8.locked == false && D_6.SJ == true && D_8.SJ == true)
                {
                    //更改道岔状态
                    D_6.DBJ = false; D_6.FBJ = true;
                    D_8.DBJ = false; D_8.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //6/8号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_6.blocked == true && D_8.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_6.error == 1 && D_8.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_6.locked == false && D_8.locked == false && D_6.SJ == true && D_8.SJ == true)
                {
                    //更改道岔状态
                    D_6.DBJ = true; D_6.FBJ = false;
                    D_8.DBJ = true; D_8.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //6/8号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_6.locked = true;
                D_8.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已单锁！", Color.Red);
            }
            //6/8号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_6.locked = false;
                D_8.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已单解！", Color.Green);
            }
            //6/8号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_6.blocked = true; D_8.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已封锁！", Color.Red);
            }
            //6/8号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_6.blocked = false; D_8.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "已解封！", Color.Green);
            }
        }
        private void L10_Click(object sender, EventArgs e)
        {
            //10号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_10.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_10.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_10.locked == false && D_10.SJ == true)
                {
                    //更改道岔状态
                    D_10.DBJ = false; D_10.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //10号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_10.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_10.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_10.locked == false && D_10.SJ == true)
                {
                    //更改道岔状态
                    D_10.DBJ = true; D_10.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //10号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_10.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已单锁！", Color.Red);
            }
            //10号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_10.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已单解！", Color.Green);
            }
            //10号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_10.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已封锁！", Color.Red);
            }
            //10号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_10.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "已解封！", Color.Green);
            }
        }
        private void L12_Click(object sender, EventArgs e)
        {
            //12号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_12.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_12.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_12.locked == false && D_12.SJ == true)
                {
                    //更改道岔状态
                    D_12.DBJ = false; D_12.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //12号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_12.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_12.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_12.locked == false && D_12.SJ == true)
                {
                    //更改道岔状态
                    D_12.DBJ = true; D_12.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //12号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_12.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已单锁！", Color.Red);
            }
            //12号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_12.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已单解！", Color.Green);
            }
            //12号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_12.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已封锁！", Color.Red);
            }
            //12号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_12.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "已解封！", Color.Green);
            }
        }
        private void L14_Click(object sender, EventArgs e)
        {
            //14号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_14.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_14.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_14.locked == false && D_14.SJ == true)
                {
                    //更改道岔状态
                    D_14.DBJ = false; D_14.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //14号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_14.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_14.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_14.locked == false && D_14.SJ == true)
                {
                    //更改道岔状态
                    D_14.DBJ = true; D_14.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //14号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_14.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已单锁！", Color.Red);
            }
            //14号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_14.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已单解！", Color.Green);
            }
            //14号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_14.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已封锁！", Color.Red);
            }
            //14号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_14.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "已解封！", Color.Green);
            }
        }
        private void L16_Click(object sender, EventArgs e)
        {
            //16号道岔总反位操作
            if (Btn_ZF.BackColor == Color.Yellow)
            {
                if (D_16.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_16.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_16.locked == false && D_16.SJ == true)
                {
                    //更改道岔状态
                    D_16.DBJ = false; D_16.FBJ = true;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总反位按钮
                    Btn_ZF.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已更改至反位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //16号道岔总定位操作
            if (Btn_ZD.BackColor == Color.Yellow)
            {
                if (D_16.blocked == true)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "处于封锁状态，无法操作！", Color.Red);
                }
                else if (D_16.error == 1)
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "处于四开状态，无法操作！", Color.Red);
                }
                
                else if (D_16.locked == false && D_16.SJ == true)
                {
                    //更改道岔状态
                    D_16.DBJ = true; D_16.FBJ = false;
                    //依据道岔状态更新显示
                    UpdateSwitch();
                    //复原总定位按钮
                    Btn_ZD.BackColor = Color.White;
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已更改至定位！", Color.Green);
                }
                else
                {
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已锁闭，无法进行定反位操作！", Color.Red);
                }
            }
            //16号道岔单锁操作
            if (Btn_DCDS.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_16.locked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单锁按钮
                Btn_DCDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已单锁！", Color.Red);
            }
            //16号道岔单解操作
            if (Btn_DCDJ.BackColor == Color.Yellow)
            {
                //更改道岔锁闭状态
                D_16.locked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原单解按钮
                Btn_DCDJ.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已单解！", Color.Green);
            }
            //16号道岔封锁操作
            if (Btn_DCFS.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_16.blocked = true;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔封锁按钮
                Btn_DCFS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已封锁！", Color.Red);
            }
            //16号道岔解封操作
            if (Btn_DCJF.BackColor == Color.Yellow)
            {
                //更改道岔封锁状态
                D_16.blocked = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                //复原道岔解封按钮
                Btn_DCJF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "已解封！", Color.Green);
            }
        }
        //定时器
        //各道岔定时器
        private void D_2_4_timer_Tick(object sender, EventArgs e)
        {
            if (DG2_2.Visible == false && DG2_3.Visible == false && DG4_2.Visible == false && DG4_3.Visible == false)
            {
                DG2_2.Visible = true; DG2_3.Visible = true; DG4_2.Visible = true; DG4_3.Visible = true;
            }
            else if (DG2_2.Visible == true && DG2_3.Visible == true && DG4_2.Visible == true && DG4_3.Visible == true)
            {
                DG2_2.Visible = false; DG2_3.Visible = false; DG4_2.Visible = false; DG4_3.Visible = false;
            }
        }
        private void D_6_8_timer_Tick(object sender, EventArgs e)
        {
            if (DG6_2.Visible == false && DG6_3.Visible == false && DG8_2.Visible == false && DG8_3.Visible == false)
            {
                DG6_2.Visible = true; DG6_3.Visible = true; DG8_2.Visible = true; DG8_3.Visible = true;
            }
            else if (DG6_2.Visible == true && DG6_3.Visible == true && DG8_2.Visible == true && DG8_3.Visible == true)
            {
                DG6_2.Visible = false; DG6_3.Visible = false; DG8_2.Visible = false; DG8_3.Visible = false;
            }
        }
        private void D_10_timer_Tick(object sender, EventArgs e)
        {
            if (DG10_2.Visible == false && DG10_3.Visible == false)
            {
                DG10_2.Visible = true; DG10_3.Visible = true;
            }
            else if (DG10_2.Visible == true && DG10_3.Visible == true)
            {
                DG10_2.Visible = false; DG10_3.Visible = false;
            }
        }
        private void D_12_timer_Tick(object sender, EventArgs e)
        {
            if (DG12_2.Visible == false && DG12_3.Visible == false)
            {
                DG12_2.Visible = true; DG12_3.Visible = true;
            }
            else if (DG12_2.Visible == true && DG12_3.Visible == true)
            {
                DG12_2.Visible = false; DG12_3.Visible = false;
            }
        }
        private void D_14_timer_Tick(object sender, EventArgs e)
        {
            if (DG14_2.Visible == false && DG14_3.Visible == false)
            {
                DG14_2.Visible = true; DG14_3.Visible = true;
            }
            else if (DG14_2.Visible == true && DG14_3.Visible == true)
            {
                DG14_2.Visible = false; DG14_3.Visible = false;
            }
        }
        private void D_16_timer_Tick(object sender, EventArgs e)
        {
            if (DG16_2.Visible == false && DG16_3.Visible == false)
            {
                DG16_2.Visible = true; DG16_3.Visible = true;
            }
            else if (DG16_2.Visible == true && DG16_3.Visible == true)
            {
                DG16_2.Visible = false; DG16_3.Visible = false;
            }
        }
        //灯丝断丝定时器
        private void DS_timer_Tick(object sender, EventArgs e)
        {
            if(S_1.FillColor == Color.Red)
            {
                S_1.FillColor = Color.Black;
            }
            else
            {
                S_1.FillColor= Color.Red;
            }
        }
        //按下调车按钮信号机名称闪烁
        private void DC_timer_Tick(object sender, EventArgs e)
        {
            if(XD_6.pressed == true)
            {
                if (LD6.Visible)
                {
                    LD6.Visible = false;
                }
                else
                {
                    LD6.Visible = true;
                }
            }
            if (XD_8.pressed == true)
            {
                if (LD8.Visible)
                {
                    LD8.Visible = false;
                }
                else
                {
                    LD8.Visible = true;
                }
            }
        }

        //各进路人工解锁
        //人工解锁30s
        private void timer_30_Tick(object sender, EventArgs e)
        {
            //判断Text输出位置
            //X8人工解锁
            if (road_exist[1] && type[1] == 1)  
            {
                CD_X8.Visible = true;
                CD_X8.Text = time_30.ToString();
            }
            //X3人工解锁
            else if (road_exist[2] && type[2] == 1)
            {
                CD_X3.Visible = true;
                CD_X3.Text = time_30.ToString();
            }
            //XI人工解锁
            else if (road_exist[3] && type[3] == 1)
            {
                CD_XI.Visible = true;
                CD_XI.Text = time_30.ToString();
            }
            //X4人工解锁
            else if (road_exist[4] && type[4] == 1)
            {
                CD_X4.Visible = true;
                CD_X4.Text = time_30.ToString();
            }
            //X6人工解锁
            else if (road_exist[5] && type[5] == 1)
            {
                CD_X6.Visible = true;
                CD_X6.Text = time_30.ToString();
            }
            //D6人工解锁
            else if (road_exist[6] && type[6] == 5)
            {
                CD_D6.Visible = true;
                CD_D6.Text = time_30.ToString();
            }

            if (time_30 == 0)
            {
                //X_8->S人工解锁
                if (road_exist[1] && type[1] == 1)  
                {
                    CancelRoad(X_8, S, 1);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_30.Enabled = false;
                    CD_X8.Visible = false;
                    time_30 = 4;
                    road_exist[1] = false;
                    type[1] = 4;
                }
                //X_3->S人工解锁
                if (road_exist[2] && type[2] == 1)
                {
                    CancelRoad(X_3, S, 1);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_30.Enabled = false;
                    CD_X3.Visible = false;
                    time_30 = 4;
                    road_exist[2] = false;
                    type[2] = 4;
                }
                //X_I->S人工解锁
                if (road_exist[3] && type[3] == 1)
                {
                    CancelRoad(X_I, S, 1);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_30.Enabled = false;
                    CD_XI.Visible = false;
                    time_30 = 4;
                    road_exist[3] = false;
                    type[3] = 4;
                }
                //X_4->S人工解锁
                if (road_exist[4] && type[4] == 1)
                {
                    CancelRoad(X_4, S, 1);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_30.Enabled = false;
                    CD_X4.Visible = false;
                    time_30 = 4;
                    road_exist[4] = false;
                    type[4] = 4;
                }
                //X_6->S人工解锁
                if (road_exist[5] && type[5] == 1)
                {
                    CancelRoad(X_6, S, 1);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_30.Enabled = false;
                    CD_X6.Visible = false;
                    time_30 = 4;
                    road_exist[5] = false;
                    type[5] = 4;
                }
                //D6->D8人工解锁
                if (road_exist[6] && type[6] == 5)
                {
                    CancelRoad(XD_6, XD_8, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_30.Enabled = false;
                    CD_D6.Visible = false;
                    time_30 = 4;
                    road_exist[6] = false;
                    type[6] = 4;
                }
            }
            time_30--;
        }
        //人工解锁180s
        private void timer_180_Tick(object sender, EventArgs e)
        {
            //判断Text输出位置
            if ((road_exist[0] || road_exist[1] || road_exist[2] || road_exist[3] || road_exist[4] || road_exist[5]) && (type[0] == 0 || type[0] == 2 || type[0] == 3 || type[1] == 0 || type[1] == 2 || type[1] == 3 || type[2] == 0 || type[2] == 2 || type[2] == 3 || type[3] == 0 || type[3] == 2 || type[3] == 3 || type[4] == 0 || type[4] == 2 || type[5] == 3 || type[5] == 0 || type[5] == 2 || type[5] == 3))  //S人工解锁
            {
                CD_S.Visible = true;
                CD_S.Text = time_180.ToString();
            }
            else if (road_exist[0] && type[0] == 1)  //X_II人工解锁
            {
                CD_XII.Visible = true;
                CD_XII.Text = time_180.ToString();
            }
                
            
            if (time_180 == 0)
            {
                //S->X_II人工解锁
                if (road_exist[0] && (type[0] == 0 || type[0] == 2 || type[0] == 3))  
                {
                    CancelRoad(S, X_II, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_S.Visible = false;
                    time_180 = 6;
                    road_exist[0] = false;
                    type[0] = 4;
                }
                //S->X_8人工解锁
                if (road_exist[1] && (type[1] == 0 || type[1] == 2 || type[1] == 3))  
                {
                    CancelRoad(S, X_8, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_S.Visible = false;
                    time_180 = 6;
                    road_exist[1] = false;
                    type[1] = 4;
                }
                //S->X_3人工解锁
                if (road_exist[2] && (type[2] == 0 || type[2] == 2 || type[2] == 3))
                {
                    CancelRoad(S, X_3, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_S.Visible = false;
                    time_180 = 6;
                    road_exist[2] = false;
                    type[2] = 4;
                }
                //S->X_I人工解锁
                if (road_exist[3] && (type[3] == 0 || type[3] == 2 || type[3] == 3))
                {
                    CancelRoad(S, X_I, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_S.Visible = false;
                    time_180 = 6;
                    road_exist[3] = false;
                    type[3] = 4;
                }
                //S->X_4人工解锁
                if (road_exist[4] && (type[4] == 0 || type[4] == 2 || type[4] == 3))
                {
                    CancelRoad(S, X_4, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_S.Visible = false;
                    time_180 = 6;
                    road_exist[4] = false;
                    type[4] = 4;
                }
                //S->X_6人工解锁
                if (road_exist[5] && (type[5] == 0 || type[5] == 2 || type[5] == 3))
                {
                    CancelRoad(S, X_6, 0);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_S.Visible = false;
                    time_180 = 6;
                    road_exist[5] = false;
                    type[5] = 4;
                }
                //X_II->S人工解锁
                if (road_exist[0] && type[0] == 1)  
                {
                    CancelRoad(X_II, S, 1);
                    OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "进路已人工解锁！", Color.Green);
                    timer_180.Enabled = false;
                    CD_XII.Visible = false;
                    time_180 = 6;
                    road_exist[0] = false;
                    type[0] = 4;
                }
            }
            time_180--;
        }

        //道岔右键菜单设置
        //道岔四开操作
        private void DCSK_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            //2/4号道岔
            if(p.X >= L2.Location.X && p.X <= L2.Location.X + L2.Size.Width + DCSK.Width && p.Y >= L2.Location.Y && p.Y <= L2.Location.Y + L2.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_2.error = 1; D_4.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "四开！", Color.Red);
            }
            //2/4号道岔
            if (p.X >= L4.Location.X && p.X <= L4.Location.X + L4.Size.Width + DCSK.Width && p.Y >= L4.Location.Y && p.Y <= L4.Location.Y + L4.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_2.error = 1; D_4.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "四开！", Color.Red);
            }
            //6/8号道岔
            if (p.X >= L6.Location.X && p.X <= L6.Location.X + L6.Size.Width + DCSK.Width && p.Y >= L6.Location.Y && p.Y <= L6.Location.Y + L6.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_6.error = 1; D_8.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "四开！", Color.Red);
            }
            //6/8号道岔
            if (p.X >= L8.Location.X && p.X <= L8.Location.X + L8.Size.Width + DCSK.Width && p.Y >= L8.Location.Y && p.Y <= L8.Location.Y + L8.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_6.error = 1; D_8.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "四开！", Color.Red);
            }
            //10号道岔
            if (p.X >= L10.Location.X && p.X <= L10.Location.X + L10.Size.Width + DCSK.Width && p.Y >= L10.Location.Y && p.Y <= L10.Location.Y + L10.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_10.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "四开！", Color.Red);
            }
            //12号道岔
            if (p.X >= L12.Location.X && p.X <= L12.Location.X + L12.Size.Width + DCSK.Width && p.Y >= L12.Location.Y && p.Y <= L12.Location.Y + L12.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_12.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "四开！", Color.Red);
            }
            //14号道岔
            if (p.X >= L14.Location.X && p.X <= L14.Location.X + L14.Size.Width + DCSK.Width && p.Y >= L14.Location.Y && p.Y <= L14.Location.Y + L14.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_14.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "四开！", Color.Red);
            }
            //16号道岔
            if (p.X >= L16.Location.X && p.X <= L16.Location.X + L16.Size.Width + DCSK.Width && p.Y >= L16.Location.Y && p.Y <= L16.Location.Y + L16.Size.Height + DCSK.Height)
            {
                //更改道岔状态
                D_16.error = 1;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "四开！", Color.Red);
            }
        }
        //道岔失表操作
        private void DCSB_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            //2/4号道岔
            if (p.X >= L2.Location.X && p.X <= L2.Location.X + L2.Size.Width + DCSB.Width && p.Y >= L2.Location.Y + DCSB.Height && p.Y <= L2.Location.Y + L2.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_2_4_timer.Enabled = false;
                //更改道岔状态
                D_2.error = 2; D_4.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "失表！", Color.Red);
                //S->XII已办理进路内存在道岔失表
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在道岔失表
                if (road_exist[1] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                
            }
            //2/4号道岔
            if (p.X >= L4.Location.X && p.X <= L4.Location.X + L4.Size.Width + DCSB.Width && p.Y >= L4.Location.Y + DCSB.Height && p.Y <= L4.Location.Y + L4.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_2_4_timer.Enabled = false;
                //更改道岔状态
                D_2.error = 2; D_4.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "失表！", Color.Red);
                //S->XII已办理进路内存在道岔失表
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在道岔失表
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                
            }
            //6/8号道岔
            if (p.X >= L6.Location.X && p.X <= L6.Location.X + L6.Size.Width + DCSB.Width && p.Y >= L6.Location.Y + DCSB.Height && p.Y <= L6.Location.Y + L6.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_6_8_timer.Enabled = false;
                //更改道岔状态
                D_6.error = 2; D_8.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "失表！", Color.Red);
                //S->XII已办理进路内存在道岔失表
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在道岔失表
                if (road_exist[1] && type[1] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                
            }
            //6/8号道岔
            if (p.X >= L8.Location.X && p.X <= L8.Location.X + L8.Size.Width + DCSB.Width && p.Y >= L8.Location.Y + DCSB.Height && p.Y <= L8.Location.Y + L8.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_6_8_timer.Enabled = false;
                //更改道岔状态
                D_6.error = 2; D_8.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "失表！", Color.Red);
                //S->XII已办理进路内存在道岔失表
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在道岔失表
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                
            }
            //10号道岔
            if (p.X >= L10.Location.X && p.X <= L10.Location.X + L10.Size.Width + DCSB.Width && p.Y >= L10.Location.Y + DCSB.Height && p.Y <= L10.Location.Y + L10.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_10_timer.Enabled = false;
                //更改道岔状态
                D_10.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "失表！", Color.Red);
                //S->XII已办理进路内存在道岔失表
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在道岔失表
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                
            }
            //12号道岔
            if (p.X >= L12.Location.X && p.X <= L12.Location.X + L12.Size.Width + DCSB.Width && p.Y >= L12.Location.Y + DCSB.Height && p.Y <= L12.Location.Y + L12.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_12_timer.Enabled = false;
                //更改道岔状态
                D_12.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "失表！", Color.Red);
            }
            //14号道岔
            if (p.X >= L14.Location.X && p.X <= L14.Location.X + L14.Size.Width + DCSB.Width && p.Y >= L14.Location.Y + DCSB.Height && p.Y <= L14.Location.Y + L14.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_14_timer.Enabled = false;
                //更改道岔状态
                D_14.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "失表！", Color.Red);
                //S->X8已办理进路内存在道岔失表
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                
            }
            //16号道岔
            if (p.X >= L16.Location.X && p.X <= L16.Location.X + L16.Size.Width + DCSB.Width && p.Y >= L16.Location.Y + DCSB.Height && p.Y <= L16.Location.Y + L16.Size.Height + DCSK.Height + DCSB.Height)
            {
                D_16_timer.Enabled = false;
                //更改道岔状态
                D_16.error = 2;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "失表！", Color.Red);
                //S->XII已办理进路内存在道岔失表
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                
            }

        }
        //道岔故障恢复
        private void DCGZHF_Click(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            //2/4号道岔
            if (p.X >= L2.Location.X && p.X <= L2.Location.X + L2.Size.Width + DCGZHF.Width && p.Y >= L2.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L2.Location.Y + L2.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_2.error = 0; D_4.error = 0;
                D_2.DBJ = true; D_2.FBJ = false; D_4.DBJ = true; D_4.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_2.name + "故障恢复！", Color.Green);
            }
            //2/4号道岔
            if (p.X >= L4.Location.X && p.X <= L4.Location.X + L4.Size.Width + DCGZHF.Width && p.Y >= L4.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L4.Location.Y + L4.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_2.error = 0; D_4.error = 0;
                D_2.DBJ = true; D_2.FBJ = false; D_4.DBJ = true; D_4.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_4.name + "故障恢复！", Color.Green);
            }
            //6/8号道岔
            if (p.X >= L6.Location.X && p.X <= L6.Location.X + L6.Size.Width + DCGZHF.Width && p.Y >= L6.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L6.Location.Y + L6.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_6.error = 0; D_8.error = 0;
                D_6.DBJ = true; D_6.FBJ = false; D_8.DBJ = true; D_8.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_6.name + "故障恢复！", Color.Green);
            }
            //6/8号道岔
            if (p.X >= L8.Location.X && p.X <= L8.Location.X + L8.Size.Width + DCGZHF.Width && p.Y >= L8.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L8.Location.Y + L8.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_6.error = 0; D_8.error = 0;
                D_6.DBJ = true; D_6.FBJ = false; D_8.DBJ = true; D_8.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_8.name + "故障恢复！", Color.Green);
            }
            //10号道岔
            if (p.X >= L10.Location.X && p.X <= L10.Location.X + L10.Size.Width + DCGZHF.Width && p.Y >= L10.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L10.Location.Y + L10.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_10.error = 0;
                D_10.DBJ = true; D_10.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_10.name + "故障恢复！", Color.Green);
            }
            //12号道岔
            if (p.X >= L12.Location.X && p.X <= L12.Location.X + L12.Size.Width + DCGZHF.Width && p.Y >= L12.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L12.Location.Y + L12.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_12.error = 0;
                D_12.DBJ = true; D_12.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_12.name + "故障恢复！", Color.Green);
            }
            //14号道岔
            if (p.X >= L14.Location.X && p.X <= L14.Location.X + L14.Size.Width + DCGZHF.Width && p.Y >= L14.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L14.Location.Y + L14.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_14.error = 0;
                D_14.DBJ = true; D_14.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_14.name + "故障恢复！", Color.Green);
            }
            //16号道岔
            if (p.X >= L16.Location.X && p.X <= L16.Location.X + L16.Size.Width + DCGZHF.Width && p.Y >= L16.Location.Y + DCSB.Height + DCGZHF.Height && p.Y <= L16.Location.Y + L16.Size.Height + DCSK.Height + DCSB.Height + DCGZHF.Height)
            {
                //更改道岔状态
                D_16.error = 0;
                D_16.DBJ = true; D_16.FBJ = false;
                //依据道岔状态更新显示
                UpdateSwitch();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + D_16.name + "故障恢复！", Color.Green);
            }
        }

        //轨道区段右键菜单设置
        //轨道区段占用
        private void GZY_Click(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            //S接近
            if(p.X >= JJ.X1 && p.X <= JJ.X2 + GZY.Width && p.Y >= JJ.Y1 && p.Y <= JJ.Y1 + GZY.Height)
            {
                //改变轨道区段状态
                S_approach.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + S_approach.name + "占用！", Color.Red);
            }
            //SF接近
            if (p.X >= JJF.X1 && p.X <= JJF.X2 + GZY.Width && p.Y >= JJF.Y1 && p.Y <= JJF.Y1 + GZY.Height)
            {
                //改变轨道区段状态
                SF_approach.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + SF_approach.name + "占用！", Color.Red);
            }
            //IBG
            if(p.X >= LIBG.Location.X && p.X <= LIBG.Location.X + LIBG.Width + GZY.Width && p.Y >= LIBG.Location.Y && p.Y <= LIBG.Location.Y + LIBG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_IBG.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_IBG.name + "占用！", Color.Red);
            }
            //2DG
            if (p.X >= L2DG.Location.X && p.X <= L2DG.Location.X + L2DG.Width + GZY.Width && p.Y >= L2DG.Location.Y && p.Y <= L2DG.Location.Y + L2DG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_2.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_2.name + "占用！", Color.Red);
            }
            //4DG
            if (p.X >= L4DG.Location.X && p.X <= L4DG.Location.X + L4DG.Width + GZY.Width && p.Y >= L4DG.Location.Y && p.Y <= L4DG.Location.Y + L4DG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_4.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_4.name + "占用！", Color.Red);
            }
            //6-14DG
            if (p.X >= L6_14DG.Location.X && p.X <= L6_14DG.Location.X + L6_14DG.Width + GZY.Width && p.Y >= L6_14DG.Location.Y && p.Y <= L6_14DG.Location.Y + L6_14DG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_6_14.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_6_14.name + "占用！", Color.Red);
            }
            //8DG
            if (p.X >= L8DG.Location.X && p.X <= L8DG.Location.X + L8DG.Width + GZY.Width && p.Y >= L8DG.Location.Y && p.Y <= L8DG.Location.Y + L8DG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_8.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_8.name + "占用！", Color.Red);
            }
            //12DG
            if (p.X >= L12DG.Location.X && p.X <= L12DG.Location.X + L12DG.Width + GZY.Width && p.Y >= L12DG.Location.Y && p.Y <= L12DG.Location.Y + L12DG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_12.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_12.name + "占用！", Color.Red);
            }
            //16DG
            if (p.X >= L16DG.Location.X && p.X <= L16DG.Location.X + L16DG.Width + GZY.Width && p.Y >= L16DG.Location.Y && p.Y <= L16DG.Location.Y + L16DG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_16.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_16.name + "占用！", Color.Red);
            }
            //3G
            if (p.X >= L3G.Location.X && p.X <= L3G.Location.X + L3G.Width + GZY.Width && p.Y >= L3G.Location.Y && p.Y <= L3G.Location.Y + L3G.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_3G.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_3G.name + "占用！", Color.Red);
            }
            //IG
            if (p.X >= LIG.Location.X && p.X <= LIG.Location.X + LIG.Width + GZY.Width && p.Y >= LIG.Location.Y && p.Y <= LIG.Location.Y + LIG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_IG.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_IG.name + "占用！", Color.Red);
            }
            //IIG
            if (p.X >= LIIG.Location.X && p.X <= LIIG.Location.X + LIIG.Width + GZY.Width && p.Y >= LIIG.Location.Y && p.Y <= LIIG.Location.Y + LIIG.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_IIG.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_IIG.name + "占用！", Color.Red);
            }
            //4G
            if (p.X >= L4G.Location.X && p.X <= L4G.Location.X + L4G.Width + GZY.Width && p.Y >= L4G.Location.Y && p.Y <= L4G.Location.Y + L4G.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_4G.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_4G.name + "占用！", Color.Red);
            }
            //6G
            if (p.X >= L6G.Location.X && p.X <= L6G.Location.X + L6G.Width + GZY.Width && p.Y >= L6G.Location.Y && p.Y <= L6G.Location.Y + L6G.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_6G.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_6G.name + "占用！", Color.Red);
            }
            //8G
            if (p.X >= L8G.Location.X && p.X <= L8G.Location.X + L8G.Width + GZY.Width && p.Y >= L8G.Location.Y && p.Y <= L8G.Location.Y + L8G.Height + GZY.Height)
            {
                //改变轨道区段状态
                DG_8G.state = 2;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_8G.name + "占用！", Color.Red);
            }
        }
        //轨道区段故障
        private void GGZ_Click(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            //S接近
            if (p.X >= JJ.X1 && p.X <= JJ.X2 + GGZ.Width && p.Y >= JJ.Y1 + GZY.Height && p.Y <= JJ.Y1 + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                S_approach.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + S_approach.name + "故障！", Color.Red);
            }
            //SF接近
            if (p.X >= JJF.X1 && p.X <= JJF.X2 + GGZ.Width && p.Y >= JJF.Y1 + GZY.Height && p.Y <= JJF.Y1 + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                SF_approach.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + SF_approach.name + "故障！", Color.Red);
            }
            //IBG
            if (p.X >= LIBG.Location.X && p.X <= LIBG.Location.X + LIBG.Width + GGZ.Width && p.Y >= LIBG.Location.Y + GZY.Height && p.Y <= LIBG.Location.Y + LIBG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_IBG.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_IBG.name + "故障！", Color.Red);
            }
            //2DG
            if (p.X >= L2DG.Location.X && p.X <= L2DG.Location.X + L2DG.Width + GGZ.Width && p.Y >= L2DG.Location.Y + GZY.Height && p.Y <= L2DG.Location.Y + L2DG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_2.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_2.name + "故障！", Color.Red);
            }
            //4DG
            if (p.X >= L4DG.Location.X && p.X <= L4DG.Location.X + L4DG.Width + GGZ.Width && p.Y >= L4DG.Location.Y + GZY.Height && p.Y <= L4DG.Location.Y + L4DG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_4.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_4.name + "故障！", Color.Red);
                //S->XII已办理进路内存在轨道区段故障
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在轨道区段故障
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                //S->X3已办理进路内存在轨道区段故障
                if (road_exist[2] && type[2] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_3.XJ = false;
                    UpdateSignal();
                }
                //S->XI已办理进路内存在轨道区段故障
                if (road_exist[3] && type[3] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_I.XJ = false;
                    UpdateSignal();
                }
                //S->X4已办理进路内存在轨道区段故障
                if (road_exist[4] && type[4] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_4.XJ = false;
                    UpdateSignal();
                }
                //S->X6已办理进路内存在轨道区段故障
                if (road_exist[5] && type[5] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_6.XJ = false;
                    UpdateSignal();
                }
            }
            //6-14DG
            if (p.X >= L6_14DG.Location.X && p.X <= L6_14DG.Location.X + L6_14DG.Width + GGZ.Width && p.Y >= L6_14DG.Location.Y + GZY.Height && p.Y <= L6_14DG.Location.Y + L6_14DG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_6_14.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_6_14.name + "故障！", Color.Red);
                //S->XII已办理进路内存在轨道区段故障
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X8已办理进路内存在轨道区段故障
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                    UpdateSignal();
                }
                //S->X3已办理进路内存在轨道区段故障
                if (road_exist[2] && type[2] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_3.XJ = false;
                    UpdateSignal();
                }
                //S->XI已办理进路内存在轨道区段故障
                if (road_exist[3] && type[3] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_I.XJ = false;
                    UpdateSignal();
                }
                //S->X4已办理进路内存在轨道区段故障
                if (road_exist[4] && type[4] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_4.XJ = false;
                    UpdateSignal();
                }
                //S->X6已办理进路内存在轨道区段故障
                if (road_exist[5] && type[5] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_6.XJ = false;
                    UpdateSignal();
                }
            }
            //8DG
            if (p.X >= L8DG.Location.X && p.X <= L8DG.Location.X + L8DG.Width + GGZ.Width && p.Y >= L8DG.Location.Y + GZY.Height && p.Y <= L8DG.Location.Y + L8DG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_8.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_8.name + "故障！", Color.Red);
                //S->X3已办理进路内存在轨道区段故障
                if (road_exist[2] && type[2] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_3.XJ = false;
                    UpdateSignal();
                }
                //S->XI已办理进路内存在轨道区段故障
                if (road_exist[3] && type[3] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_I.XJ = false;
                    UpdateSignal();
                }
            }
            //12DG
            if (p.X >= L12DG.Location.X && p.X <= L12DG.Location.X + L12DG.Width + GGZ.Width && p.Y >= L12DG.Location.Y + GZY.Height && p.Y <= L12DG.Location.Y + L12DG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_12.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_12.name + "故障！", Color.Red);
                //S->X3已办理进路内存在轨道区段故障
                if (road_exist[2] && type[2] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_3.XJ = false;
                    UpdateSignal();
                }
                //S->XI已办理进路内存在轨道区段故障
                if (road_exist[3] && type[3] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_I.XJ = false;
                    UpdateSignal();
                }
            }
            //16DG
            if (p.X >= L16DG.Location.X && p.X <= L16DG.Location.X + L16DG.Width + GGZ.Width && p.Y >= L16DG.Location.Y + GZY.Height && p.Y <= L16DG.Location.Y + L16DG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_16.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_16.name + "故障！", Color.Red);
                //S->XII已办理进路内存在轨道区段故障
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                    UpdateSignal();
                }
                //S->X4已办理进路内存在轨道区段故障
                if (road_exist[4] && type[4] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_4.XJ = false;
                    UpdateSignal();
                }
            }
            //3G
            if (p.X >= L3G.Location.X && p.X <= L3G.Location.X + L3G.Width + GGZ.Width && p.Y >= L3G.Location.Y + GZY.Height && p.Y <= L3G.Location.Y + L3G.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_3G.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_3G.name + "故障！", Color.Red);
            }
            //IG
            if (p.X >= LIG.Location.X && p.X <= LIG.Location.X + LIG.Width + GGZ.Width && p.Y >= LIG.Location.Y + GZY.Height && p.Y <= LIG.Location.Y + LIG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_IG.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_IG.name + "故障！", Color.Red);
            }
            //IIG
            if (p.X >= LIIG.Location.X && p.X <= LIIG.Location.X + LIIG.Width + GGZ.Width && p.Y >= LIIG.Location.Y + GZY.Height && p.Y <= LIIG.Location.Y + LIIG.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_IIG.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_IIG.name + "故障！", Color.Red);
            }
            //4G
            if (p.X >= L4G.Location.X && p.X <= L4G.Location.X + L4G.Width + GGZ.Width && p.Y >= L4G.Location.Y + GZY.Height && p.Y <= L4G.Location.Y + L4G.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_4G.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_4G.name + "故障！", Color.Red);
            }
            //6G
            if (p.X >= L6G.Location.X && p.X <= L6G.Location.X + L6G.Width + GGZ.Width && p.Y >= L6G.Location.Y + GZY.Height && p.Y <= L6G.Location.Y + L6G.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_6G.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_6G.name + "故障！", Color.Red);
            }
            //8G
            if (p.X >= L8G.Location.X && p.X <= L8G.Location.X + L8G.Width + GGZ.Width && p.Y >= L8G.Location.Y + GZY.Height && p.Y <= L8G.Location.Y + L8G.Height + GZY.Height + GGZ.Height)
            {
                //改变轨道区段状态
                DG_8G.error = 1;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：已设置" + DG_8G.name + "故障！", Color.Red);
            }
        }
        //轨道区段恢复
        private void GHF_Click(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            //S接近
            if (p.X >= JJ.X1 && p.X <= JJ.X2 + GHF.Width && p.Y >= JJ.Y1 + GZY.Height + GGZ.Height && p.Y <= JJ.Y1 + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                S_approach.state = 0;
                S_approach.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + S_approach.name + "已恢复！", Color.Green);
            }
            //SF接近
            if (p.X >= JJF.X1 && p.X <= JJF.X2 + GHF.Width && p.Y >= JJF.Y1 + GZY.Height + GGZ.Height && p.Y <= JJF.Y1 + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                SF_approach.state = 0;
                SF_approach.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + SF_approach.name + "已恢复！", Color.Green);
            }
            //IBG
            if (p.X >= LIBG.Location.X && p.X <= LIBG.Location.X + LIBG.Width + GHF.Width && p.Y >= LIBG.Location.Y + GZY.Height + GGZ.Height && p.Y <= LIBG.Location.Y + LIBG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_IBG.state = 0;
                DG_IBG.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_IBG.name + "已恢复！", Color.Green);
            }
            //2DG
            if (p.X >= L2DG.Location.X && p.X <= L2DG.Location.X + L2DG.Width + GHF.Width && p.Y >= L2DG.Location.Y + GZY.Height + GGZ.Height && p.Y <= L2DG.Location.Y + L2DG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_2.state = 0;
                DG_2.error= 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_2.name + "已恢复！", Color.Green);
            }
            //4DG
            if (p.X >= L4DG.Location.X && p.X <= L4DG.Location.X + L4DG.Width + GHF.Width && p.Y >= L4DG.Location.Y + GZY.Height + GGZ.Height && p.Y <= L4DG.Location.Y + L4DG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_4.state = 0;
                DG_4.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_4.name + "已恢复！", Color.Green);
            }
            //6-14DG
            if (p.X >= L6_14DG.Location.X && p.X <= L6_14DG.Location.X + L6_14DG.Width + GHF.Width && p.Y >= L6_14DG.Location.Y + GZY.Height + GGZ.Height && p.Y <= L6_14DG.Location.Y + L6_14DG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_6_14.state = 0;
                DG_6_14.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_6_14.name + "已恢复！", Color.Green);
            }
            //8DG
            if (p.X >= L8DG.Location.X && p.X <= L8DG.Location.X + L8DG.Width + GHF.Width && p.Y >= L8DG.Location.Y + GZY.Height + GGZ.Height && p.Y <= L8DG.Location.Y + L8DG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_8.state = 0;
                DG_8.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_8.name + "已恢复！", Color.Green);
            }
            //12DG
            if (p.X >= L12DG.Location.X && p.X <= L12DG.Location.X + L12DG.Width + GHF.Width && p.Y >= L12DG.Location.Y + GZY.Height + GGZ.Height && p.Y <= L12DG.Location.Y + L12DG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_12.state = 0;
                DG_12.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_12.name + "已恢复！", Color.Green);
            }
            //16DG
            if (p.X >= L16DG.Location.X && p.X <= L16DG.Location.X + L16DG.Width + GHF.Width && p.Y >= L16DG.Location.Y + GZY.Height + GGZ.Height && p.Y <= L16DG.Location.Y + L16DG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_16.state = 0;
                DG_16.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_16.name + "已恢复！", Color.Green);
            }
            //3G
            if (p.X >= L3G.Location.X && p.X <= L3G.Location.X + L3G.Width + GHF.Width && p.Y >= L3G.Location.Y + GZY.Height + GGZ.Height && p.Y <= L3G.Location.Y + L3G.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_3G.state = 0;
                DG_3G.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_3G.name + "已恢复！", Color.Green);
            }
            //IG
            if (p.X >= LIG.Location.X && p.X <= LIG.Location.X + LIG.Width + GHF.Width && p.Y >= LIG.Location.Y + GZY.Height + GGZ.Height && p.Y <= LIG.Location.Y + LIG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_IG.state = 0;
                DG_IG.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_IG.name + "已恢复！", Color.Green);
            }
            //IIG
            if (p.X >= LIIG.Location.X && p.X <= LIIG.Location.X + LIIG.Width + GHF.Width && p.Y >= LIIG.Location.Y + GZY.Height + GGZ.Height && p.Y <= LIIG.Location.Y + LIIG.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_IIG.state = 0;
                DG_IIG.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_IIG.name + "已恢复！", Color.Green);
            }
            //4G
            if (p.X >= L4G.Location.X && p.X <= L4G.Location.X + L4G.Width + GHF.Width && p.Y >= L4G.Location.Y + GZY.Height + GGZ.Height && p.Y <= L4G.Location.Y + L4G.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_4G.state = 0;
                DG_4G.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_4G.name + "已恢复！", Color.Green);
            }
            //6G
            if (p.X >= L6G.Location.X && p.X <= L6G.Location.X + L6G.Width + GHF.Width && p.Y >= L6G.Location.Y + GZY.Height + GGZ.Height && p.Y <= L6G.Location.Y + L6G.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_6G.state = 0;
                DG_6G.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_6G.name + "已恢复！", Color.Green);
            }
            //8G
            if (p.X >= L8G.Location.X && p.X <= L8G.Location.X + L8G.Width + GHF.Width && p.Y >= L8G.Location.Y + GZY.Height + GGZ.Height && p.Y <= L8G.Location.Y + L8G.Height + GZY.Height + GGZ.Height + GHF.Height)
            {
                //改变轨道区段状态
                DG_8G.state = 0;
                DG_8G.error = 0;
                //根据状态更新显示
                UpdateTrack();
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + DG_8G.name + "已恢复！", Color.Green);
            }
        }
        //S灯丝操作
        private void S_1_Click(object sender, EventArgs e)
        {
            //S灯丝断丝
            if(Btn_DSDS.BackColor == Color.Yellow)
            {
                S.DJ = false;
                UpdateSignal();
                Btn_DSDS.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "S灯丝断丝！", Color.Red);
                //S->XII已办理进路内无法开放进站信号
                if (road_exist[0] && type[0] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_II.XJ = false;
                }
                //S->X8已办理进路内无法开放进站信号
                if (road_exist[1] && type[1] == 0)  
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_8.XJ = false;
                }
                //S->X3已办理进路内无法开放进站信号
                if (road_exist[2] && type[2] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_3.XJ = false;
                }
                //S->XI已办理进路内无法开放进站信号
                if (road_exist[3] && type[3] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; XD_8.XJ = false; X_I.XJ = false;
                }
                //S->X4已办理进路内无法开放进站信号
                if (road_exist[4] && type[4] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_4.XJ = false;
                }
                //S->X6已办理进路内无法开放进站信号
                if (road_exist[5] && type[5] == 0)
                {
                    //关闭信号
                    S.XJ = false; XD_6.XJ = false; X_6.XJ = false;
                }
            }
            
            //S断丝恢复
            if(Btn_DSHF.BackColor == Color.Yellow)
            {
                S.DJ = true;
                UpdateSignal();
                Btn_DSHF.BackColor = Color.White;
                OutMsg(PromptWindow, DateTime.Now.ToString() + "：" + "S灯丝断丝已恢复！", Color.Red);
            }
        }

        //模拟行车功能实现
        //S->XII模拟行车
        private void timer_S_XII_Tick(object sender, EventArgs e)
        {
            //设置列车运行速度
            Train_S.Left -= 5;
            if (Train_S.Left >= JJ.X1)
            {
                //接近区段占用
                S_approach.state = 1; S_approach.LJ_2 = true;
            }
            else if (Train_S.Left <= JJ.X1 && Train_S.Right >= JJ.X1)  //列车压入信号机内方区段但未出清接近区段
            {
                //关闭始端信号机
                S.XJ = false; UpdateSignal();
                //4DG占用
                DG_4.state = 1;
            }
            else if (Train_S.Left >= DG4_1.X1 && Train_S.Right <= JJ.X1)  //列车完全进入4DG，完全离开接近区段
            {
                //接近区段解锁
                S_approach.state = 0; S_approach.LJ_1 = true; S_approach.locked = false;
                //4DG进路继电器状态改变
                DG_4.LJ_2 = true;
            }
            else if(Train_S.Left <= DG4_1.X1 && Train_S.Right >= DG4_1.X1)  //列车压入6-14DG但未出清4DG
            {
                //6-14DG占用
                DG_6_14.state = 1;
            }
            else if(Train_S.Left >= DG10_4.X1 && Train_S.Right <= DG4_1.X1)  //列车完全进入6-14DG，完全离开4DG
            {
                //4DG解锁
                DG_4.state = 0; DG_4.LJ_1 = true; DG_4.locked = false;
                //道岔解锁
                D_2.SJ = true; D_4.SJ = true;
                //D6敌对信号关闭
                XD_6.XJ = false;
                //6-14DG进路继电器状态改变
                DG_6_14.LJ_2 = true;
            }
            else if(Train_S.Left <= DG10_4.X1 && Train_S.Right >= DG10_4.X1)  //列车压入16DG但未出清6-14DG
            {
                //16DG占用
                DG_16.state = 1;
            }
            else if(Train_S.Left >= DG16_4.X1 && Train_S.Right <= DG10_4.X1)  //列车完全进入16DG，完全离开6-14DG
            {
                //6-14DG解锁
                DG_6_14.state = 0; DG_6_14.LJ_1 = true; DG_6_14.locked = false;
                //道岔解锁
                D_6.SJ = true; D_8.SJ = true;
                D_10.SJ = true; D_14.SJ = true;
                //16DG进路继电器状态改变
                DG_16.LJ_2 = true;
            }
            else if(Train_S.Left <= DG16_4.X1 && Train_S.Right >= DG16_4.X1)  //列车压入IIG但未出清16DG
            {
                //IIG占用
                DG_IIG.state = 1;
            }
            else if(Train_S.Left >= GIIG.X1 + 100 &&Train_S.Right <= DG16_4.X1)  //列车完全进入IIG，接车完毕
            {
                //16DG解锁
                DG_16.state = 0; DG_16.LJ_1 = true; DG_16.locked = false;
                //道岔解锁
                D_16.SJ = true;
                //XII敌对信号关闭
                X_II.XJ = false;
            }
            else
            {
                timer_S_XII.Enabled = false;
            }
            //实时更新轨道区段状态
            UpdateTrack();
        }
    }
}
