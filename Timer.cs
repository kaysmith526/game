using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace StarterGame
{
    public class Timer
    {
        private string _name;
        private int _startingSeconds;
        private int _remainingSeconds;
        private bool _isStarted;
        private bool _isPaused;
        private bool _isFinished;

        public string Name { get { return _name; } }
        public int StartingSeconds { get { return _startingSeconds; } }
        public int RemainingSeconds { get { return _remainingSeconds; } }
        public bool IsStarted { get { return _isStarted; } }
        public bool IsPaused { get { return _isPaused; } }
        public bool IsFinished { get { return _isFinished; } }

        private Thread _timerThread;

        public Timer(int startingSeconds) : this("Main", startingSeconds) { }

        public Timer(string name, int startingSeconds)
        {
            _name = name;
            _startingSeconds = startingSeconds;
            Reset(); //makes timer ready to be started
        }

        private void CountDown()
        {
            Notification notification;

            _isStarted = true;
            notification = new Notification("TimerStarted_" + Name);
            notification.Object = this;
            NotificationCenter.Instance.PostNotification(notification);

            //continues looping as long as it has time left
            while (_remainingSeconds > 0)
            {
                //only decreases time if timer is not paused
                if (!_isPaused)
                {
                    _remainingSeconds--;

                    notification = new Notification("TimerTicked_" + Name);
                    notification.Object = this;
                    NotificationCenter.Instance.PostNotification(notification);
                }
                Thread.Sleep(1000); //pauses code for  1 second (1000 milliseconds) 
            }
            _isFinished = true;

            notification = new Notification("TimerFinished_" + Name);
            notification.Object = this;
            NotificationCenter.Instance.PostNotification(notification);
        }

        public void Reset()
        {
            _remainingSeconds = _startingSeconds;
            _isStarted = false;
            _isPaused = false;
            _isFinished = false;

            _timerThread = new Thread(CountDown);
        }

        //starts thread if it has never started before
        public void Start()
        {
            if (_timerThread == null || _isStarted)
                return;

            _timerThread.Start();
        }

        public void Pause()
        {
            _isPaused = true;
            Notification notification = new Notification("TimerPaused_" + Name);
            notification.Object = this;
            NotificationCenter.Instance.PostNotification(notification);
        }

        public void Resume()
        {
            _isPaused = false;
            Notification notification = new Notification("TimerResumed_" + Name);
            notification.Object = this;
            NotificationCenter.Instance.PostNotification(notification);
        }
    }
}
