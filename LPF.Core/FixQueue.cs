using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPF.Core
{
    public class FixQueue<T>
       where T : class
    {

        protected readonly Queue<T> InnerQueue = new Queue<T>();

        #region  事件

        public delegate bool BeforeEnQueueEventHandler(T target);
        /// <summary>
        /// The before en queue event.
        /// </summary>
        public event BeforeEnQueueEventHandler BeforeEnQueueEvent;

        #endregion

        #region  构造函数

        /// <summary>
        /// 最大队列数量
        /// </summary>
        protected int InnerFixCount = 5;

        public FixQueue(int fixCount)
        {
            this.InnerFixCount = fixCount;
        }

        #endregion


        #region  属性
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {

                return this.InnerQueue.Count;

            }
        }

        /// <summary>
        /// Gets a value indicating whether has value.
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this.Count != 0;
            }
        }

        #endregion


        /// <summary>
        /// The en queue.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        public void EnQueue(T target)
        {
            if (InnerQueue.Count >= InnerFixCount)
            {
                this.InnerQueue.Dequeue();
            }
            if (this.BeforeEnQueueEvent != null)
            {
                if (this.BeforeEnQueueEvent(target))
                {
                    this.InnerQueue.Enqueue(target);
                }
            }
            else
            {
                this.InnerQueue.Enqueue(target);
            }

        }
        /// <summary>
        /// The de queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T DeQueue()
        {

            if (this.InnerQueue.Count > 0)
            {
                return this.InnerQueue.Dequeue();
            }

            return default(T);

        }


        /// <summary>
        /// The en queue.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        public bool Any(Func<T, bool> predicate)
        {
            return this.InnerQueue.Any(model => predicate(model));
        }

        /// <summary>
        /// The en queue.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        public void ForEach(Action<T> action)
        {
            foreach (var model in this.InnerQueue)
            {
                action(model);
            }
        }








    }
}
