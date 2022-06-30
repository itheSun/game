using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogFM
{
    public interface IView
    {
        void OnShow();
        void OnHide();
        void OnActive();
        void OnBlock();
    }
}
