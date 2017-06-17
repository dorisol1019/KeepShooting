//
//  EventListenerGesture.h
//  EventListenerGesture.cpp
//
//  Licence: MIT
//  Copyright (c) 2015 Shotaro Takagi (poowww)
//  https://github.com/Poowww/EventListenerGesture
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;

namespace KeepShooting.Models
{
    public class CCEventListenerGesture : CCEventListenerTouchOneByOne
    {
        public enum SwipeDirection
        {
            NONE,
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public enum GestureType
        {
            NONE,
            TAP,
            //LONG_TAP未実装
            LONG_TAP,
            SWIPE
        }

        public static CCEventListenerGesture Create()
        {
            var ret = new CCEventListenerGesture();
            ret.Init();
            if (ret != null && ret.Init())
            {

            }
            else
            {
                //CC_SAFE_DELETE(ret);
            }
            return ret;
        }

        //        virtual ~EventListenerGesture(){ }

        //        virtual EventListenerGesture* clone() override{ }

        public override bool IsAvailable
        {
            get
            {
                if (OnTouchBegan == null)
                {
                    //                    CCASSERT(false, "Invalid EventListenerGesture!");
                    return false;
                }

                return true;
            }
        }


        public void SetLongTapThreshouldSeconds(float threshould)
        {
            if (threshould > 0)
            {
                _longTapThresholdSeconds = threshould;
            }
        }
        public void SetSwipeThreshouldDistance(float threshould)
        {
            if (threshould > 0)
            {
                _swipeThresholdDistance = threshould;
            }
        }

        public Action<CCVector2> OnTap { get; set; } = null;

        [Obsolete("LongTapは未実装です")]
        public Action<CCVector2> OnLongTapBegan { get; set; } = null;
        [Obsolete("LongTapは未実装です")]
        public Action<CCVector2> OnLongTapEnded { get; set; } = null;
        public Action<SwipeDirection> OnSwipeing { get; set; } = null;
        public Action<SwipeDirection> OnSwipeEnded { get; set; } = null;

        public bool IsTouched => _isTouched;

        //protected:
        protected CCEventListenerGesture()
        {
        }

        protected bool Init()
        {
            OnTouchBegan = (touch, cCEvent) =>
            {
                if (_isTouched)
                {
                    return false;
                }

                _isTouched = true;
                _touchStartTime = DateTime.Now;
                _touchStartPos = touch.Location;
                _touchNowPos = touch.Location;

                //                    this
                //                    CCScheduler
                //Director::getInstance()->getScheduler()->schedule(schedule_selector(EventListenerGesture::_updateInTouch), this, 0.05f, false);
                return true;
            };
            this.
            OnTouchCancelled = (_touch, ccEvent) =>
            {
                _isTouched = false;
                _gestureType = GestureType.NONE;
            };

            OnTouchMoved = (touch, ccEvent) =>
            {
                //if (_gestureType != GestureType.NONE)
                //{
                //    return;
                //}

                _touchNowPos = touch.Location;
                var xDiff = _touchNowPos.X - _touchStartPos.X;
                var xDiffAbs = Math.Abs((int)(xDiff));
                var yDiff = _touchNowPos.Y - _touchStartPos.Y;
                var yDiffAbs = Math.Abs((int)(yDiff));
                var swipeDirection = SwipeDirection.NONE;

                if (xDiffAbs >= yDiffAbs)
                {
                    if (xDiffAbs > _swipeThresholdDistance)
                        swipeDirection = xDiff >= 0 ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
                }
                else
                {
                    if (yDiffAbs > _swipeThresholdDistance)
                        swipeDirection = yDiff >= 0 ? SwipeDirection.UP : SwipeDirection.DOWN;
                }

                if (swipeDirection != SwipeDirection.NONE)
                {
                    _gestureType = GestureType.SWIPE;
                    OnSwipeing?.Invoke(swipeDirection);
                }
                _swipeDirection = swipeDirection;
            };

            OnTouchEnded = (touch, ccEvent) =>
            {
                if (_gestureType == GestureType.SWIPE)
                {
                    OnSwipeEnded?.Invoke(_swipeDirection);
                    _swipeDirection = SwipeDirection.NONE;
                }
                else
                    if (_gestureType == GestureType.NONE)
                {
                    OnTap?.Invoke(touch.Delta);
                }
                else if (_gestureType == GestureType.LONG_TAP)
                {
                    OnLongTapEnded.Invoke(touch.Delta);
                }

                //Director::getInstance()->getScheduler()->
                //    unschedule(schedule_selector(EventListenerGesture::_updateInTouch), this);
                _gestureType = GestureType.NONE;
                _isTouched = false;
            };

            return true;

        }
        ///private:
        //using CCEventListenerTouchOneByOne::LISTENER_ID;
        //using CCEventListenerTouchOneByOne::onTouchBegan;
        //using CCEventListenerTouchOneByOne::onTouchMoved;
        //using CCEventListenerTouchOneByOne::onTouchEnded;
        //using CCEventListenerTouchOneByOne::onTouchCancelled;

        float _longTapThresholdSeconds = DefaultLongTapThresholdSeconds;
        float _swipeThresholdDistance = DefaultSwipeThresholdDistance;

        //        std::chrono::system_clock::time_point _touchStartTime{ }
        //        std::chrono::system_clock::time_point _beforeTapEndTime{ }
        DateTime _touchStartTime;
        DateTime _beforeTapEndTime;
        CCVector2 _touchStartPos;
        CCVector2 _touchNowPos;
        GestureType _gestureType = GestureType.NONE;
        SwipeDirection _swipeDirection = SwipeDirection.NONE;
        bool _isTouched = false;
        void _updateInTouch(float f)
        {
        }

        const float DefaultLongTapThresholdSeconds = 0.1f;
        const float DefaultSwipeThresholdDistance = 10.0f;

    }
}
