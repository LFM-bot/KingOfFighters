using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

//播放动图的方法参考于该博客  https://www.cnblogs.com/cpw999cn/archive/2009/02/07/1385885.html
namespace TheKingOfFighters
{
    /**/
    /// <summary>      
    /// 表示一类带动画功能的图像，自定义类。      
    /// </summary>      
    public class AnimateImage
    {
        #region 私有字段
        //动图
        Image image;
        //动图每一帧的尺寸
        FrameDimension frameDimension;
        //是否为动画图片
        bool mCanAnimate;
        //  总的帧数           当前播放帧
        int mFrameCount = 1, mCurrentFrame = 0;
        #endregion

        #region 公有字段

        //动画当前帧发生改变时使用，OnFameChanged委托执行，将迫使重绘。         
        public event EventHandler<EventArgs> OnFrameChanged;
        //标志该动画是否播放结束
        public bool _over = true;
        //表示动画播放速度，即下一帧为 当前帧+_actionSpeed 帧
        public int _actionSpeed = 1;

        /**/
        /// <summary>      
        /// 图片。      
        /// </summary>      
        public Image Image
        {
            get { return image; }
        }

        /**/
        /// <summary>      
        /// 是否动画。      
        /// </summary>      
        public bool CanAnimate
        {
            get { return mCanAnimate; }
        }

        /**/
        /// <summary>      
        /// 总帧数。      
        /// </summary>      
        public int FrameCount
        {
            get { return mFrameCount; }
        }

        /**/
        /// <summary>      
        /// 播放的当前帧。      
        /// </summary>      
        public int CurrentFrame
        {
            get { return mCurrentFrame; }
        }
        #endregion

        //构造方法  
        public AnimateImage(Image img)
        {
            image = img;
            lock (image)
            {
                //判断该图片是否是动图
                mCanAnimate = ImageAnimator.CanAnimate(image);
                if (mCanAnimate)
                {
                    Guid[] guid = image.FrameDimensionsList;
                    //保存帧尺寸
                    frameDimension = new FrameDimension(guid[0]);
                    //保存帧数
                    mFrameCount = image.GetFrameCount(frameDimension);
                }
            }
        }
 

        //停止播放    
        public void Stop()
        {
            if (mCanAnimate)
            {
                lock (image)
                {
                        ImageAnimator.StopAnimate(image, new EventHandler(FrameChanged1));
                }
            }
        }
        //重置动画，使之停止在第0帧位置上。       
        public void Reset(int speed)
        {
            if (mCanAnimate)
            {
                
                lock (image)
                {
                    image.SelectActiveFrame(frameDimension, 0);
                    mCurrentFrame = 0;
                }
                if(speed==1)
                    ImageAnimator.StopAnimate(image, new EventHandler(FrameChanged1));
                else 
                    ImageAnimator.StopAnimate(image, new EventHandler(FrameChangedHalf));
            }
        }
        // 播放这个动画         
        public void Play(int mode)
        {
            //由于很多动图的播放速度并不令人满意,在此写了多个方法对动图速度进行对应调整
            if (mCanAnimate)
            {
                _over = false;
                //此处使用.net自身携带的类ImageAnimator的Animate方法播放动图
                lock (image)
                {
                    //以每次跳过1帧播放，循环播放
                    if (mode == 0)
                        ImageAnimator.Animate(image, new EventHandler(FrameChanged));
                    //以每次跳过_actionSpeed帧播放，只播放一次
                    else if (mode == 1)
                        ImageAnimator.Animate(image, new EventHandler(FrameChanged1));
                    //以每次跳过_actionSpeed帧播放，只播放一次，到总数一半结束
                    else if (mode == 2)
                        ImageAnimator.Animate(image, new EventHandler(FrameChangedHalf));
                }
            }
        }
        //模式0:循环播放动画
        private void FrameChanged(object sender, EventArgs e)
        {
            //具体实现
            mCurrentFrame = mCurrentFrame + 1 >= mFrameCount ? 0 : mCurrentFrame + 1;
            lock (image)
            {
                //选择规定尺寸和索引的动图活动帧
                image.SelectActiveFrame(frameDimension, mCurrentFrame);
            }
            if (OnFrameChanged != null)
            {
                //执行委托，迫使重绘显示这一帧
                OnFrameChanged(image, e);
            }
        }

        //模式1:以每次_actionSpeed帧的速度播放，用于速度过慢的动图
        private void FrameChanged1(object sender, EventArgs e)
        {
            //如果下_actionSpeed帧大于等于总帧数，播放结束
            if (mCurrentFrame + _actionSpeed >= mFrameCount)
            {
                //记录结束状态，退出
                _over = true;
                return;
            }
            //否则播放
            mCurrentFrame = mCurrentFrame + _actionSpeed >= mFrameCount ? 0 : mCurrentFrame + _actionSpeed;
            lock (image)
            {
                //选择规定尺寸和索引的动图活动帧
                image.SelectActiveFrame(frameDimension, mCurrentFrame);
            }
            if (OnFrameChanged != null)
            {
                //执行委托，迫使重绘显示这一帧
                OnFrameChanged(image, e);
            }
        }

        //模式2:以每次_actionSpeed帧的速度播放，且接近动图一半后结束
        private void FrameChangedHalf(object sender, EventArgs e)
        {
            //具体实现
            if (mCurrentFrame + _actionSpeed >= mFrameCount/2+3)
            {
                _over = true;
                return;
            }
            mCurrentFrame = mCurrentFrame + _actionSpeed >= mFrameCount ? 0 : mCurrentFrame + _actionSpeed;
            lock (image)
            {
                //选择规定尺寸和索引的动图活动帧
                image.SelectActiveFrame(frameDimension, mCurrentFrame);
            }
            if (OnFrameChanged != null)
            {
                //执行委托，迫使重绘显示这一帧
                OnFrameChanged(image, e);
            }
        }
    }
}
