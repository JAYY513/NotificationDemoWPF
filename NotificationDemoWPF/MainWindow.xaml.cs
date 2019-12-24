using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NotificationDemoWPF.Event;

namespace NotificationDemoWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IMainEventCommnuicationHandler
    {
        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        int i = 0;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            i++;
            NotifyData data = new NotifyData();
            data.Title = "这是标题:" + i;
            data.Content = "这是手动内容 ";
            showNotify(data);
        }

        private void showNotify(NotifyData data)
        { 
            NotificationWindow dialog = new NotificationWindow();//new 一个通知
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            dialog.DataContext = data;//设置通知里要显示的数据
            dialog.Show();

            _dialogs.Add(dialog);
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }

        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 110;//此处100是NotifyWindow的高 110-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }


        private NoticeManager noticeManager;
        private void Time_Click(object sender, RoutedEventArgs e)
        {
            if (noticeManager == null)
            {
                noticeManager = new NoticeManager(this);
                btnTime.Content = "暂停弹出"; 
            }
            else
            {
                btnTime.Content = "定时弹出";
                noticeManager.Dispose();
            }
        }


        public void EventNotify(EventData eventData)
        {
            if (eventData.EventNotify == EventNotifyType.New)
            {
                var data = eventData.Data as NotifyData;
                SendMessage(data);
            }
            else
            {
                MessageBox.Show("其他定时消息");
            }
        }

        /// <summary>
        /// 发出通知
        /// </summary>
        /// <param name="data"></param>
        void SendMessage(NotifyData data)
        {
            //此处调用Invoke，否则会报错：“ 调用线程必须为 STA,因为许多 UI 组件都需要 ”。
            App.Current.Dispatcher.Invoke(() =>
            {
                showNotify(data);
            });
        }
    }
}
