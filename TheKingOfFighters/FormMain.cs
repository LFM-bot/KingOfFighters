using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Runtime.InteropServices;

namespace TheKingOfFighters
{
    //游戏状态
    public enum GameState
    {
        Waiting = 0, Start = 1, Over = 2
    }
    public partial class FormMain : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetAsyncKeyState(int keycode);
        #region 私有字段
        //自定义修改过颜色的生命进度条
        MyProgressBar pb1 = new MyProgressBar(Color.FromArgb(192, 25, 32));
        MyProgressBar pb2 = new MyProgressBar(Color.FromArgb(192, 25, 32));

        MyProgressBar pbPower1 = new MyProgressBar(Color.FromArgb(49, 223, 2));
        MyProgressBar pbPower2 = new MyProgressBar(Color.FromArgb(49, 223, 2));

        //用于控制图片的闪烁切换
        private bool _change = true;
        //背景动图
        private AnimateImage _imageBack;
        //玩家
        private Player _player = new Player();
        //测试电脑
        private Enemy _enemy = new Enemy();
        //游戏状态
        private GameState _gameState = GameState.Waiting;
        //背景左上角x坐标
        private int _backLocation = -335;
        //敌人序列
        private List<Enemy> _enemyList = new List<Enemy>();
        #endregion

        public FormMain()
        {
            InitializeComponent();
            //加载动图
            _imageBack = new AnimateImage(Image.FromFile("images\\游戏背景.gif"));
            //添加委托（方法作另一个方法的参数，调用委托时，会执行调用列表中的所有方法）
            _imageBack.OnFrameChanged += new EventHandler<EventArgs>(image_OnFrameChanged);
            _player._Image.OnFrameChanged += new EventHandler<EventArgs>(image_OnFrameChanged);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //设置生命条1
            pb1.Parent = progressBar1;
            pb1.Minimum = 0;//进度条显示最小值
            pb1.Maximum = 100;//进度条显示最大值
            pb1.Width = progressBar1.Width;
            pb1.Height = progressBar1.Height;
            pb1.BackColor = Color.Black;
            pb1.Value = 100;

            //设置生命条2
            pb2.Parent = progressBar2;
            pb2.Minimum = 0;//进度条显示最小值
            pb2.Maximum = 100;//进度条显示最大值
            pb2.Width = progressBar1.Width;
            pb2.Height = progressBar1.Height;
            pb2.BackColor = Color.Black;
            pb2.Value = 100;
            //设置能量条1
            pbPower1.Parent = progressBar3;
            pbPower1.Minimum = 0;//进度条显示最小值
            pbPower1.Maximum = 100;//进度条显示最大值
            pbPower1.Width = progressBar3.Width;
            pbPower1.Height = progressBar3.Height;
            pbPower1.BackColor = Color.Black;
            pbPower1.Value = 0;

            //设置能量条2
            pbPower2.Parent = progressBar4;
            pbPower2.Minimum = 0;//进度条显示最小值
            pbPower2.Maximum = 100;//进度条显示最大值
            pbPower2.Width = progressBar4.Width;
            pbPower2.Height = progressBar4.Height;
            pbPower2.BackColor = Color.Black;
            pbPower2.Value = 0;

            //播放视频
            MediaStart();
            //添加至Enemy序列
            _enemyList.Add(_enemy);
        }
        //重绘
        void image_OnFrameChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        //播放视频
        private void MediaStart()
        {
            MediaPlayer1.Visible = true;
            //修改大小模式
            MediaPlayer1.Location = new Point(0, 0);
            MediaPlayer1.Size = new Size(884, 461);
            //播放
            MediaPlayer1.URL = "media\\开场动画3.mp4";
            MediaPlayer1.Ctlcontrols.play();
            //焦点
            MediaPlayer1.Focus();
        }
        
        //动画结束
        private void MediaEnd()
        {
            MediaPlayer1.Ctlcontrols.stop();
            MediaPlayer1.Visible = false;
            //打开定时器切换图片
            timerEnter.Enabled = true;
            //焦点
            this.Focus();
        }

        //动图结束,开始游戏
        private void GameStart()
        {
            //游戏状态
            _gameState = GameState.Start;
            //关闭闪烁效果
            timerEnter.Enabled = false;
            //循环播放背景
            _imageBack.Play(0);
            //播放开始音效
            //装载声音文件(需要添加System.Media命名空间)
            SoundPlayer soundPlay = new SoundPlayer("media\\ReadyGo.wav");
            soundPlay.Play();

            //循环播放BGM
            Thread thread = new Thread(new ThreadStart(PlayThread));
            thread.Start();
            //打开人物移动定时器
            timerPlayerMove.Enabled = true;
            //打开检测定时器
            timerDetection.Enabled = true;
            //开场动作
            _player.ActionStart();
            _enemy.ActionStart();

            progressBar1.Visible = true;
            progressBar2.Visible = true;

            progressBar3.Visible = true;
            progressBar4.Visible = true;

            pbPlayer1.Visible = true;
            pbPlayer2.Visible = true;
        }

        //切换图片，达到闪烁效果
        private void timerEnter_Tick(object sender, EventArgs e)
        {
            //切换为图1
            if (_change)
            {
                pictureBox1.Image = Properties.Resources.背景图1;
                _change = false;
            }
            //切换为图二
            else
            {
                pictureBox1.Image = Properties.Resources.背景图1_0;
                _change = true; ;
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            //当游戏状态为Waiting时,按下enter开始游戏
            if (_gameState == GameState.Waiting)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GameStart();
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //如果游戏开始
            if (_gameState==GameState.Start)
            {
                lock (_imageBack.Image)
                {
                    //以（1554,461)大小在(_backLocation,0)绘制普通背景
                    e.Graphics.DrawImage(_imageBack.Image, _backLocation, 0, 1554, 461);
                }

                if (_player.ActionList[(int)(dir.O)])
                {
                    //绘制大招的背景
                    e.Graphics.DrawImage(Properties.Resources.卢卡尔背景,0, 0,884,461);
                }
                else if (_enemy.ActionList[(int)(dir.O)])
                {
                    //绘制大招的背景
                    e.Graphics.DrawImage(Properties.Resources.克里斯背景, 0, 0, 884, 461);
                }

                //绘制人物
                _player.Draw(e.Graphics);

                _enemy.Draw(e.Graphics);

            }
            else if (_gameState == GameState.Over)
            {
                lock (_imageBack.Image)
                {
                    //以（1554,461)大小在(_backLocation,0)绘制普通背景
                    e.Graphics.DrawImage(_imageBack.Image, _backLocation, 0, 1554, 461);
                    //绘制KO
                    e.Graphics.DrawImage(Properties.Resources.KO, 442 - 165, 70,343,120);
                }

                //绘制人物
                _player.Draw(e.Graphics);

                _enemy.Draw(e.Graphics);
                
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //观察点坐标，测试用
            //this.Text = "x:" + e.X + "  y:" + e.Y;
        }

        private void timerPlayerMove_Tick(object sender, EventArgs e)
        {
            //通过x坐标确定左右位置
            if (_player.Location.X <= _enemy.Location.X)
            {
                _player.Pos = Position.Left;
                _enemy.Pos = Position.right;
            }
            else
            {
                _player.Pos = Position.right;
                _enemy.Pos = Position.Left;
            }

            //每次把动作清为无
                _player.Direction = dir.none;
            //每次把动作清为无
                _enemy.Direction = dir.none;

            //蹲下
            bool KeySquat = (((ushort)GetAsyncKeyState((int)Keys.X)) & 0xffff) != 0;
            //若按了蹲下键，且此刻Player动作可被打断
            if (KeySquat == true && _player.IfBreak) 
            { 
                //蹲下动作，详见定义
                _player.ActionSquat();
            }
         
            bool KeyJump = (((ushort)GetAsyncKeyState((int)Keys.L)) & 0xffff) != 0;
            //若按了跳键，且此刻Player动作可被打断
            if (KeyJump == true && _player.IfBreak)
            {
                //同上，详见定义
                _player.ActionJump();
            }
            bool KeyLeft = (((ushort)GetAsyncKeyState((int)Keys.A)) & 0xffff) != 0;
            //若按了左键，且此刻Player动作可被打断
            if (KeyLeft == true && _player.IfBreak) 
            {
                //同上，详见定义
                _player.ActionLeft();
            }

            bool KeyRight = (((ushort)GetAsyncKeyState((int)Keys.D)) & 0xffff) != 0;
            if (KeyRight == true && _player.IfBreak) 
            {
                //同上，详见定义
                _player.ActionRight();
            }
            bool KeyDown = (((ushort)GetAsyncKeyState((int)Keys.S)) & 0xffff) != 0;
            if (KeyDown == true && _player.IfBreak)
            {
                _player.Direction = dir.down;
                //如果同时在水平方向有运动，则不播放动图
                if (!_player.ActionList[(int)(_player.Direction)] && !KeyLeft && !KeyRight)
                {
                    _player.ActionDown();
                }
                _player.Move();
            }
            bool KeyUp = (((ushort)GetAsyncKeyState((int)Keys.W)) & 0xffff) != 0;
            if (KeyUp == true && _player.IfBreak)
            {
                _player.Direction = dir.up;
                //如果同时在水平方向有运动，则不播放动图
                if (!_player.ActionList[(int)(_player.Direction)] && !KeyRight && !KeyLeft)
                {
                    _player.ActionUp();
                }
                _player.Move();
            }
            //按了大招
            bool KeyO = (((ushort)GetAsyncKeyState((int)Keys.O)) & 0xffff) != 0;
            if (KeyO == true&&_player.IfBreak)
            {
                ///同上，详见定义
                _player.ActionO();
            }
            //按了J
            bool KeyJ = (((ushort)GetAsyncKeyState((int)Keys.J)) & 0xffff) != 0;
            if (KeyJ == true && _player.IfBreak)
            {
                //同上，详见定义
                _player.ActionJ(ref _enemyList);
            }
            //按了K
            bool KeyK = (((ushort)GetAsyncKeyState((int)Keys.K)) & 0xffff) != 0;
            if (KeyK == true && _player.IfBreak)
            {
                //同上，详见定义
                _player.ActionK(ref _enemyList);
            }
            //什么都不按
            if (_player.Direction == dir.none && _player.IfBreak)
            {
                //同上，详见定义
                _player.ActionNothing();
            }

            //以下按键部分为enemy
            bool Key3 = (((ushort)GetAsyncKeyState((int)Keys.D3)) & 0xffff) != 0;
            //若按了跳键，且此刻_enemy动作可被打断
            if (Key3 == true && _enemy.IfBreak)
            {
                //同上，详见定义
                _enemy.ActionJump();
            }
            bool KeyL = (((ushort)GetAsyncKeyState((int)Keys.Left)) & 0xffff) != 0;
            //若按了左键，且此刻Player动作可被打断
            if (KeyL == true && _enemy.IfBreak)
            {
                //同上，详见定义
                _enemy.ActionLeft();
            }

            bool KeyR = (((ushort)GetAsyncKeyState((int)Keys.Right)) & 0xffff) != 0;
            if (KeyR == true && _enemy.IfBreak)
            {
                //同上，详见定义
                _enemy.ActionRight();
            }
            bool KeyD = (((ushort)GetAsyncKeyState((int)Keys.Down)) & 0xffff) != 0;
            if (KeyD == true && _enemy.IfBreak)
            {
                _enemy.Direction = dir.down;
                //如果同时在水平方向有运动，则不播放动图
                if (!_enemy.ActionList[(int)(_enemy.Direction)] && !KeyR && !KeyL)
                {
                    _enemy.ActionDown();
                }
                _enemy.Move();
            }
            bool KeyU = (((ushort)GetAsyncKeyState((int)Keys.Up)) & 0xffff) != 0;
            if (KeyU == true && _enemy.IfBreak)
            {
                _enemy.Direction = dir.up;
                //如果同时在水平方向有运动，则不播放动图
                if (!_enemy.ActionList[(int)(_enemy.Direction)] && !KeyR && !KeyL)
                {
                    _enemy.ActionUp();
                }
                _enemy.Move();
            }
            //按了大招
            bool Key6 = (((ushort)GetAsyncKeyState((int)Keys.D6)) & 0xffff) != 0;
            if (Key6 == true && _enemy.IfBreak)
            {
                ///同上，详见定义
                _enemy.ActionO();
            }
            //按了J
            bool Key1 = (((ushort)GetAsyncKeyState((int)Keys.D1)) & 0xffff) != 0;
            if (Key1 == true && _enemy.IfBreak)
            {
                //同上，详见定义
                _enemy.ActionJ(ref _player);
            }
            //按了K
            bool Key2 = (((ushort)GetAsyncKeyState((int)Keys.D2)) & 0xffff) != 0;
            if (Key2 == true && _enemy.IfBreak)
            {
                //同上，详见定义
                _enemy.ActionK(ref _player);
            }
            //什么都不按
            if (_enemy.Direction == dir.none && _enemy.IfBreak)
            {
                //同上，详见定义
                _enemy.ActionNothing();
            }

            
            //动作结束后，设置人物状态为可打断状态
            if (_player._Image._over == true)
            {
                //修正大招之后人物的位置偏移
                if (_player.ActionList[(int)(dir.O)])
                    if(_player.Pos==Position.Left)
                        _player.Location = new Point(_player.Location.X + 55, _player.Location.Y);
                    else
                        _player.Location = new Point(_player.Location.X - 55, _player.Location.Y);
                //可打断
                _player.IfBreak = true;
            }
            if (_enemy._Image._over == true)
            {
                //修正大招之后人物的位置偏移
                if (_enemy.ActionList[(int)(dir.O)])
                    if(_enemy.Pos==Position.right)
                        _enemy.Location = new Point(_enemy.Location.X - 170, _enemy.Location.Y);
                    else
                        _enemy.Location = new Point(_enemy.Location.X + 125, _enemy.Location.Y);
                //可打断
                _enemy.IfBreak = true;
            }
            //人物走出边界的处理，20作为左端边界
            if (_player.Location.X < 20)
            {
                //背景的x坐标增加，且不得大于0
                if (_backLocation + 20 - _player.Location.X< 0)
                {
                    _backLocation += 20 - _player.Location.X;
                }
                //固定人物在x=20的位置
                _player.Location = new Point(20, _player.Location.Y);
            }
            //864作为右端边界
            else if (_player.Location.X > 864)
            {
                //背景x坐标减少，且不得小于-670
                if (_backLocation - (_player.Location.X-864) >-670)
                {
                    _backLocation -= _player.Location.X - 864;
                }
                //固定人物在x=864的位置
                _player.Location = new Point(864, _player.Location.Y);
            }
            //人物走出边界的处理，20作为左端边界
            if (_enemy.Location.X < 20)
            {
                //背景的x坐标增加，且不得大于0
                if (_backLocation + 20 - _enemy.Location.X < 0)
                {
                    _backLocation += 20 - _enemy.Location.X;
                }
                //固定人物在x=20的位置
                _enemy.Location = new Point(20, _enemy.Location.Y);
            }
            //864作为右端边界
            else if (_enemy.Location.X > 864)
            {
                //背景x坐标减少，且不得小于-670
                if (_backLocation - (_enemy.Location.X - 864) > -670)
                {
                    _backLocation -= _enemy.Location.X - 864;
                }
                //固定人物在x=864的位置
                _enemy.Location = new Point(864, _enemy.Location.Y);
            }

            //重绘，由于定时器时刻重绘了pictureBox,所以即使没有委托的重绘依旧可播放动图
            pictureBox1.Invalidate();
        }

        //MediaPlayer播放状态改变事件
        private void MediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //如果游戏状态为结束
            if (MediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                //播放停止
                MediaPlayer1.Ctlcontrols.stop();
                //设为不可见
                MediaPlayer1.Visible = false;
                //开始游戏
                GameStart();
            }
        }

        //按键按下事件
        private void MediaPlayer1_KeyDownEvent(object sender, AxWMPLib._WMPOCXEvents_KeyDownEvent e)
        {
            //若按下esc，则视频播放结束
            if (e.nKeyCode == (ushort)Keys.Escape && MediaPlayer1.Ctlcontrols.currentPosition <= 38.5)
            {
                MediaEnd();
            }
            //如果当前播放位置大于38.5秒且按下enter，则直接进入游戏
            else if (e.nKeyCode == (ushort)Keys.Enter && MediaPlayer1.Ctlcontrols.currentPosition > 38.5)
            {
                MediaPlayer1.Ctlcontrols.stop();
                MediaPlayer1.Visible = false;
                GameStart();
            }
            //通过左右快慢进视频(测试用)
            else if (e.nKeyCode == (ushort)Keys.Left)
                MediaPlayer1.Ctlcontrols.currentPosition -= 5;
            else if (e.nKeyCode == (ushort)Keys.Right)
                MediaPlayer1.Ctlcontrols.currentPosition += 5;
        }
        //进行大招的检测
        private void timerDetection_Tick(object sender, EventArgs e)
        {
            //判断是否大招碰撞
            foreach (Enemy E in _enemyList)
            {
                //若在Player攻击范围内，且碰撞值大于Enemy
                if (_player.IfAttackSuccess(E.Location) && _player.CollisionIntensity > E.CollisionIntensity && _player.ActionList[(int)dir.O]&&_player.Direction!=dir.beaten)
                {
                    //播放打击音效
                    //装载声音文件(需要添加System.Media命名空间)
                    SoundPlayer soundPlay = new SoundPlayer("media\\Hit.wav");
                    soundPlay.Play();
                    E.ActionBeaten(_player.Damage);
                }
                //如果开启大招没打到对方
                else if (!_player.IfAttackSuccess(E.Location) && _player.ActionList[(int)dir.O])
                {
                    _player.CollisionIntensity = 4;
                    _player.AttackRange = new Point(150, 30);
                }
                if (E.IfAttackSuccess(_player.Location) && _player.CollisionIntensity < E.CollisionIntensity && E.ActionList[(int)dir.O] && _player.Direction != dir.beaten)
                {
                    //播放打击音效
                    //装载声音文件(需要添加System.Media命名空间)
                    SoundPlayer soundPlay = new SoundPlayer("media\\Hit.wav");
                    soundPlay.Play();
                    _player.ActionBeaten(E.Damage);
                }
                else if (!E.IfAttackSuccess(_player.Location) && E.ActionList[(int)dir.O])
                {
                    _player.CollisionIntensity = 3;
                    _player.AttackRange = new Point(180, 25);
                }
            }
            //跟新血量条
            if (_player.HealthPoint <= 0||_enemy.HealthPoint <= 0)
            {
                //游戏状态
                _gameState = GameState.Over;

                if (_player.HealthPoint <= 0)
                {
                    pb1.Value = 0;
                    //胜利动画
                    _player.ActionLose();
                    _player._Image.OnFrameChanged += new EventHandler<EventArgs>(image_OnFrameChanged);

                    _enemy.ActionWin();
                    _enemy._Image.OnFrameChanged += new EventHandler<EventArgs>(image_OnFrameChanged);
                }
                else
                {
                    pb2.Value = 0;
                    //胜利动画
                    _player.ActionWin();
                    _player._Image.OnFrameChanged += new EventHandler<EventArgs>(image_OnFrameChanged);

                    _enemy.ActionLose();
                    _enemy._Image.OnFrameChanged += new EventHandler<EventArgs>(image_OnFrameChanged);
                }
                timerPlayerMove.Enabled = false;
                timerDetection.Enabled = false;
                //停止BGM
                MediaPlayer2.Ctlcontrols.stop();
                //播放KO音效
                SoundPlayer sp = new SoundPlayer("media\\KO.wav");
                sp.Play();
                
            }
            else  
            {
                pbPower1.Value = (int)(_player.PowerPoint);
                pbPower2.Value = (int)(_enemy.PowerPoint);
                pb1.Value = (int)(_player.HealthPoint);
                pb2.Value = (int)(_enemy.HealthPoint);
            }

        }

        private void MediaPlayer2_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //如果游戏状态为结束
            if (MediaPlayer2.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                Thread thread = new Thread(new ThreadStart(PlayThread));
                thread.Start();
            }
        }

        private void PlayThread()
        {

            MediaPlayer2.URL = "media\\Unlimited R.wav"; //音乐文件
            MediaPlayer2.Ctlcontrols.play();
        }
    }
}
