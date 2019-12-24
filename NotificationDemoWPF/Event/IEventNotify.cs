namespace NotificationDemoWPF.Event
{
    /// <summary>
    /// 事件通知处理接口（可省略这一层）
    /// </summary>
    public interface IMainEventCommnuicationHandler : IEventNotify
    {

    }

    /// <summary>
    /// 事件通知接口
    /// </summary>
    public interface IEventNotify
    { 
        /// <summary>
        /// 事件通知
        /// </summary>
        void EventNotify(EventData eventData);
    }
     
    /// <summary>
    /// 事件数据实体
    /// </summary>
    public class EventData
    { 
        public EventNotifyType EventNotify { get; set; } 

        public object Data { get; set; } 
    }


    public enum EventNotifyType
    {
        /// <summary>
        /// 新消息通知
        /// </summary>
        New,
        Old,
    }
}