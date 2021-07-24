using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Media;

namespace TheKingOfFighters
{
    //方向和按键，统称动作
    public enum dir
    {
        down = 0, up = 1, left = 2, right = 3, none = 4, J=5,K=6,O=7,start=8,squat=9,jump=10,beaten=11,win=12,lose=13
    };
    public enum Position
    {
        Left, right
    };
    class Player
    {
        #region 私有字段
        //人物的坐标，取动图长方形中心的位置
        private Point _location;
        //外形动画类对象
        private AnimateImage _image;
        //进行的动作
        private dir _direction;
        //人物移动速度
        private int _speed;
        //动作序列，人物正在进行的动作
        private bool[] _actionList = new bool[100];
        //动图播放模式
        private int _playMode = 1;
        //当前键位
        private dir _keyNow = dir.none;
        //动作是否可被打断
        private bool _ifBreak = true;

        #endregion

        #region 公有字段
        //面向的方向
        public Position Pos = Position.Left;
        //生命值
        public double HealthPoint=100;
        //能量值
        public double PowerPoint = 0;
        //伤害值
        public double Damage = 0;
        //攻击范围,x表水平单方向范围，y表垂直方向，上下都为y
        public Point AttackRange = new Point(0, 0);
        //是否处于被击打状态(暂时未使用)
        public bool Beaten = false;
        //碰撞强度
        public int CollisionIntensity = 0;
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public AnimateImage _Image
        {
            get { return _image; }
            set { _image = value; }
        }
        public dir Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        
        public bool[] ActionList
        {
          get { return _actionList; }
          set { _actionList = value; }
        }
        public int PlayMode
        {
            get { return _playMode; }
            set { _playMode = value; }
        }
        public dir KeyNow
        {
            get { return _keyNow; }
            set { _keyNow = value; }
        }
        public bool IfBreak
        {
            get { return _ifBreak; }
            set { _ifBreak = value; }
        }
        #endregion

        //构造方法，初始化
        public Player()
        {
            //清空动作序列
            for(int i=0;i<_actionList.Length;i++)
                _actionList[i]=false;
            _location = new Point(300, 325);
            _speed = 5;
            _image = new AnimateImage(Image.FromFile("images\\卢卡尔开场.gif"));
        }
        //位置移动的方法,在此未对左右边界做限制
        public void Move()
        {
            //控制上下移动速度为speed-1
            if (_direction == dir.down)
            {
                if (_location.Y + 55 + (_speed-1) <460)
                    _location.Y += _speed;
            }
            if (_direction == dir.up)
            {
                if(_location.Y+55-(_speed-1)>353)
                    _location.Y -= _speed;
            }
            //左右移动不变，仍为speed
            else if (_direction == dir.left)
            {
                   _location.X -= _speed;
             }
            //往右走
            else if (_direction == dir.right)
            {
                _location.X += _speed;
            }

        }

        //人物绘制方法
        public void Draw(Graphics g)
        {
            //锁定Image，否则很容易出现“对象当前正在其他地方使用。”的异常，因为AnimateImage也在使用这个Image对象。
            lock (_image.Image)
            {
                //由于动图的大小有差异，所以需对绘制动图位置作调整
                if (_actionList[(int)(dir.jump)])
                    g.DrawImage(_image.Image, _location.X - 48, _location.Y - 70 - 70);
                else if(_actionList[(int)(dir.O)])
                    g.DrawImage(_image.Image, _location.X - 48-30, _location.Y - 70-70);
                else if (_actionList[(int)(dir.start)])
                    g.DrawImage(_image.Image, _location.X - 48 - 28 , _location.Y - 70 - 11 );
                else if (_actionList[(int)(dir.K)])
                    g.DrawImage(_image.Image, _location.X - 48-16, _location.Y - 70-36);
                else if(_actionList[(int)(dir.win)])
                    g.DrawImage(_image.Image, _location.X - 48+25, _location.Y - 70+15);
                else if (_actionList[(int)(dir.lose )])
                    g.DrawImage(_image.Image, _location.X - 48 , _location.Y - 70 +20);        
                else 
                    g.DrawImage(_image.Image, _location.X - 48 , _location.Y - 70 );
                    //48与70约为Stay动图的二分之一的宽和高
            }  
           
        }

        //开场动作
        public void ActionStart()
        {
            //开场动作
            //改变动作状态为 开始
            Direction = dir.start;
            //动作不可被打断
            IfBreak = false;
            for (int i = 0; i < ActionList.Length; i++)
                ActionList[i] = false;
            ActionList[(int)(Direction)] = true;
            //导入开场动图
            _Image = new AnimateImage(Image.FromFile("images\\卢卡尔开场.gif"));//
            //动图结束为否
            _Image._over = false;
            //播放模式为1
            PlayMode = 1;
            //播放速度 为2
            _Image._actionSpeed = 2;
            //播放动图
            _Image.Play(PlayMode);
        }

        //蹲下动作
        public void ActionSquat()
        {
            //设置动作为蹲
            Direction = dir.squat;
            //如果是第一次按下蹲，即动作序列ActionList中没有蹲
            if (!ActionList[(int)(Direction)])
            {
                //动作设置为不可打断
                IfBreak = false;
                //清空动作序列
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                //蹲动作为真
                ActionList[(int)(Direction)] = true;

                //播放蹲的动画
                if(Pos==Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Squat.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Squat.gif"));
                PlayMode = 2;
                _Image._actionSpeed = 3;
                _Image.Play(PlayMode);
            }
            //若不是第一次按下，则播放保持蹲下的图片达到长按一直蹲的效果
            else
            {
                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Squat1.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Squat1.gif"));
                _Image.Play(PlayMode);
            }
        }

        //起跳动作
        public void ActionJump()
        {
            Direction = dir.jump;
            if (!ActionList[(int)(Direction)])
            {
                //动作设置为不可打断
                IfBreak = false;
                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;
                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Jump.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Jump.gif"));
                //模式1
                PlayMode = 1;
                //速度为4
                _Image._actionSpeed = 4;
                _Image.Play(PlayMode);
            }
        }
        //向左移动
        public void ActionLeft()
        {
            //碰撞强度为0
            CollisionIntensity = 0;
            Direction = dir.left;
            if (!ActionList[(int)(Direction)])
            {
                IfBreak = true;

                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;

                ActionList[(int)(Direction)] = true;
                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Goback.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔GoForward.gif"));
                PlayMode = 0;
                _Image.Play(PlayMode);
            }
            Move();
        }
        //向右移动
        public void ActionRight()
        {
            //碰撞强度为0
            CollisionIntensity = 0;
            Direction = dir.right;
            if (!ActionList[(int)(Direction)])
            {
                IfBreak = true;

                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;

                //循环播放走路姿势
                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔GoForward.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Goback.gif"));
                PlayMode = 0;
                _Image.Play(PlayMode);
            }
            Move(); 
        }
        //向上移动
        public void ActionUp()
        {
            IfBreak = true;

            //清空
            for (int i = 0; i < ActionList.Length; i++)
                ActionList[i] = false;
            ActionList[(int)(Direction)] = true;

            PlayMode = 0;
            _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔GoForward.gif"));
            _Image.Play(PlayMode);
        }
        //向下移动
        public void ActionDown()
        {
            IfBreak = true;

            //清空
            for (int i = 0; i < ActionList.Length; i++)
                ActionList[i] = false;
            ActionList[(int)(Direction)] = true;

            PlayMode = 0;
            _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔GoForward.gif"));
            _Image.Play(PlayMode);
        }

        //大招
        public void ActionO()
        {
            //能量过少不发释放大招
            if (PowerPoint < 60)
                return;
            //碰撞强度为10
            CollisionIntensity = 11;
            Direction = dir.O;
            if (!ActionList[(int)(Direction)])
            {
                //扣除能量
                PowerPoint -= 50;
                //伤害设定
                Damage = 3;
                //攻击范围设定
                AttackRange = new Point(150, 50);
                IfBreak = false;

                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;
                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔SpecialSkill1.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔SpecialSkill1.gif"));
                PlayMode = 1;
                _Image._actionSpeed = 3;
                _Image.Play(PlayMode);
            }
        }

        //出拳动作
        public void ActionJ(ref List<Enemy> _enemyList)
        {
            //碰撞强度为4
            CollisionIntensity = 4;
            Direction = dir.J;
            if (!ActionList[(int)(Direction)])
            {
                //伤害设定
                Damage = 4;
                //攻击范围设定
                AttackRange = new Point(80, 40);

                IfBreak = false;
                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;
                if (Pos == Position.Left)
                     _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Fist.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Fist.gif"));
                //选择模式1，可调整速度
                PlayMode = 1;
                //速度为5
                _Image._actionSpeed = 5;
                _Image.Play(PlayMode);

                //在此检测避免了一次攻击产生多次Beaten时间的异常
                //判断是否碰撞
                foreach (Enemy E in _enemyList)
                {
                    //若在Player攻击范围内，且碰撞值大于Enemy
                    if (IfAttackSuccess(E.Location) && CollisionIntensity > E.CollisionIntensity)
                    {
                        E.ActionBeaten(Damage);
                        PowerPoint = PowerPoint + 10 >= 100 ? 100 : PowerPoint + 10;
                    }
                    else if (E.IfAttackSuccess(Location) && CollisionIntensity < E.CollisionIntensity)
                        ActionBeaten(E.Damage);
                }
            }
        }

        //出腿动作
        public void ActionK(ref List<Enemy> _enemyList)
        {
            //碰撞强度为6
            CollisionIntensity = 6;
            Direction = dir.K;
            if (!ActionList[(int)(Direction)])
            {
                //伤害设定
                Damage = 9;
                //攻击范围设定s
                AttackRange = new Point(100, 20);

                IfBreak = false;
                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;

                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Kick.gif"));
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Kick.gif"));
                PlayMode = 2;
                _Image._actionSpeed = 3;
                _Image.Play(PlayMode);

                //在此检测避免了一次攻击产生多次Beaten时间的异常
                //判断是否碰撞
                foreach (Enemy E in _enemyList)
                {
                    //若在Player攻击范围内，且碰撞值大于Enemy
                    if (IfAttackSuccess(E.Location) && CollisionIntensity > E.CollisionIntensity)
                    {
                        E.ActionBeaten(Damage);
                        PowerPoint=PowerPoint + 7 >= 100 ? 100 : PowerPoint + 7;
                    }
                    else if (E.IfAttackSuccess(Location) && CollisionIntensity < E.CollisionIntensity)
                    {
                        ActionBeaten(E.Damage);
                        //暂时不加
                    }
                }
            }
        }

        //喘气动作
        public void ActionNothing()
        {
            //碰撞强度为0
            CollisionIntensity = 0;
            if (!ActionList[(int)(Direction)])
            {
                //攻击范围设定
                AttackRange = new Point(0, 0);
                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;

                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔stay0.gif"));//
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔stay0.gif"));//
                _Image.Play(0);
                IfBreak = true;
            }
        }

        //判断是否处于攻击距离内
        public bool IfAttackSuccess(Point enemy)
        {
            //距离小于攻击范围
            if (Pos == Position.Left && enemy.X - Location.X <= AttackRange.X && Math.Abs(enemy.Y - Location.Y) < AttackRange.Y)
                return true;
            else if (Pos == Position.right && Location.X - enemy.X <= AttackRange.X && Math.Abs(enemy.Y - Location.Y) < AttackRange.Y)
                return true;
            else
                return false;
        }
        //被击中动作,被击中后的僵直
        public void ActionBeaten(double damage)
        {
            //碰撞强度为0
            CollisionIntensity = 0;
            //设置Beaten状态
            Direction = dir.beaten;
            if (!ActionList[(int)(Direction)])
            {
                //播放打击音效
                //装载声音文件(需要添加System.Media命名空间)
                SoundPlayer soundPlay = new SoundPlayer("media\\Hit.wav");
                soundPlay.Play();
                //扣除伤害
                HealthPoint -= damage;
                IfBreak = false;
                //清空
                for (int i = 0; i < ActionList.Length; i++)
                    ActionList[i] = false;
                ActionList[(int)(Direction)] = true;

                if (Pos == Position.Left)
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Beaten.gif"));//
                else
                    _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Beaten.gif"));//
                //选择模式1，可调整速度
                PlayMode = 1;
                //速度为2
                _Image._actionSpeed = 2;
                _Image.Play(PlayMode);
            }
        }

        public void ActionWin()
        {
            Direction = dir.win;
            //清空
            for (int i = 0; i < ActionList.Length; i++)
                ActionList[i] = false;
            ActionList[(int)(Direction)] = true;

            if (Pos == Position.Left)
                _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔Win.gif"));
            else
                _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔Win.gif"));
            //选择模式1，可调整速度
            PlayMode = 0;
            //速度为2
            //_Image._actionSpeed = 2;
            _Image.Play(PlayMode);
        }

        public void ActionLose()
        {
            Direction = dir.lose;
            //清空
            for (int i = 0; i < ActionList.Length; i++)
                ActionList[i] = false;
            ActionList[(int)(Direction)] = true;

            if (Pos == Position.Left)
                _Image = new AnimateImage(Image.FromFile("images\\卢卡尔L\\卢卡尔lose.gif"));
            else
                _Image = new AnimateImage(Image.FromFile("images\\卢卡尔R\\卢卡尔lose.gif"));
            //选择模式1，可调整速度
            PlayMode = 1;
            //速度为2
            _Image._actionSpeed = 1;
            _Image.Play(PlayMode);
        }
    }
}
