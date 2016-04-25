using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateAsync
{
    public class CS_StateAsync
    {
        #region 共有領域
        public enum EventCode
        {   // 状態情報
            Notify = 0,    // 0 : Notify
            Create,         // 1 : Create
            Start,          // 2 : Start
            Resume,         // 3 : Resume
            Run,            // 4 : Run
            Pause,          // 5 : Pause
            Stop,           // 6 : Stop
            Destroy,        // 7 : Destroy
            End             // 8 : End
        }
        private static EventCode _state;    // 状態情報管理
        public EventCode State
        {
            get
            {
                return (_state);
            }
            set
            {
                _state = value;
            }
        }
        private static int _job;            // 処理情報管理
        public int Job
        {
            get
            {
                return (_job);
            }
            set
            {
                _job = value;
            }
        }
        #endregion

        #region コンストラクタ
        public CS_StateAsync()
        {   // コンストラクタ
            _state = EventCode.Notify;
            _job = 0;
        }

        public async Task ClearAsync()
        {
            _state = EventCode.Notify;
        }
        public async Task StepAsync()
        {
            switch (this.State)
            {
                case EventCode.Notify:
                    this.State = EventCode.Create;      // [0]->[1]
                    break;
                case EventCode.Create:
                    this.State = EventCode.Start;       // [1]->[2]
                    break;
                case EventCode.Start:
                    this.State = EventCode.Resume;      // [2]->[3]
                    break;
                case EventCode.Resume:
                    this.State = EventCode.Run;         // [3]->[4]
                    break;
                case EventCode.Run:
                    this.State = EventCode.Pause;       // [4]->[5]
                    break;
                case EventCode.Pause:
                    this.State = EventCode.Stop;        // [5]->[6]
                    break;
                case EventCode.Stop:
                    this.State = EventCode.Destroy;     // [6]->[7]
                    break;
                case EventCode.Destroy:
                    this.State = EventCode.End;         // [7]->[8]
                    break;
                case EventCode.End:
                    this.State = EventCode.Notify;      // [8]->[0]
                    break;
                default:
                    break;
            }
        }
        public async Task StepAsync(EventCode _value)
        {
            if (_value == EventCode.Create)
            {   // On Create
                if (this.State == EventCode.Notify)
                {   // [0]->[1]
                    this.State = _value;
                }
                if (this.State == EventCode.Pause)
                {   // [5]->[1]
                    this.State = _value;
                }
                if (this.State == EventCode.Stop)
                {   // [6]->[1]
                    this.State = _value;
                }
            }
            if (_value == EventCode.Start)
            {   // On Start
                if (this.State == EventCode.Create)
                {   // [1]->[2]
                    this.State = _value;
                }
                if (this.State == EventCode.Resume)
                {   // [3]->[2]
                    this.State = _value;
                }
            }
            if (_value == EventCode.Resume)
            {   // On Resume
                if (this.State == EventCode.Start)
                {   // [2]->[3]
                    this.State = _value;
                }
                if (this.State == EventCode.Pause)
                {   // [5]->[3]
                    this.State = _value;
                }
                if (this.State == EventCode.Stop)
                {   // [6]->[3]
                    this.State = _value;
                }
            }
            if ((_value == EventCode.Run) && (this.State == EventCode.Resume))
            {   // [3]->[4]
                this.State = _value;
            }
            if ((_value == EventCode.Pause) && (this.State == EventCode.Run))
            {   // [4]->[5]
                this.State = _value;
            }
            if ((_value == EventCode.Stop) && (this.State == EventCode.Pause))
            {   // [5]->[6]
                this.State = _value;
            }
            if ((_value == EventCode.Destroy) && (this.State == EventCode.Stop))
            {   // [6]->[7]
                this.State = _value;
            }
            if ((_value == EventCode.End) && (this.State == EventCode.Destroy))
            {   // [7]->[8]
                this.State = _value;
            }
            if ((_value == EventCode.Notify) && (this.State == EventCode.End))
            {   // [8]->[0]
                this.State = _value;
            }
        }
        #endregion

        #region モジュール
        #endregion
    }
}
