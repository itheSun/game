using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DogFM.MVVM
{
    public class BaseView<T> : MonoBehaviour, IView, IBinding<T>, IObserver<IView> where T : BaseViewModel
    {
        /// <summary>
        /// 是否可见
        /// </summary>
        protected bool visible = false;

        /// <summary>
        /// 是否可交互
        /// </summary>
        protected bool interactive = true;

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        protected bool isInitialized = false;

        public bool Visible { get => this.visible; }
        public bool Interactive { get => this.interactive; }

        protected readonly BindableProperty<T> viewModel = new BindableProperty<T>();
        protected readonly PropertyBinder<T> binder = new PropertyBinder<T>();

        public T BindingContext
        {
            get => viewModel.Value;
            set
            {
                if (!isInitialized)
                {
                    Initialize();
                    isInitialized = true;
                }
                viewModel.Value = value;
            }
        }

        private void Initialize()
        {
            viewModel.OnValueChanged += OnContextChanged;
        }

        private void OnContextChanged(T old, T value)
        {
            binder.Unbind(old);
            binder.Bind(value);
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        public virtual void OnShow()
        {
            this.visible = true;
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        public virtual void OnHide()
        {
            this.visible = false;
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 激活面板
        /// </summary>
        public virtual void OnActive()
        {
            this.interactive = false;
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 冻结面板
        /// </summary>
        public virtual void OnBlock()
        {
            this.interactive = false;
            this.gameObject.SetActive(true);
        }


        public void OnNext(IView value)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}
