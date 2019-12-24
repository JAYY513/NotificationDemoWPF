using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Controls;

namespace NotificationDemoWPF.Event
{
    public class NoticeManager : IDisposable
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private readonly Timer _timer;
        public IMainEventCommnuicationHandler Handler { get; private set; }
         
        private int seconds = 1 * 3;

        public NoticeManager(IMainEventCommnuicationHandler handler)
        {
            Handler = handler;
            _timer = new Timer(seconds * 1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
             
        }

        /// <summary>
        /// 定时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        { 
            for (int i = 0; i < 3; i++)
            {
                var notifyData = new NotifyData()
                {
                    Title = "标题" + i,
                    Content = DateTime.Now.AddHours(i).ToString()

                };
                EventData eventData = new EventData()
                {
                    EventNotify = EventNotifyType.New,
                    Data = notifyData
                };
                Handler.EventNotify(eventData);
            }
        }
         
        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}