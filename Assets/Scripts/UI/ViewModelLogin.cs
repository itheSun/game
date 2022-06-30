using DogFM.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ViewModelLogin : BaseViewModel
{
    public BindableProperty<string> userName = new BindableProperty<string>();
    public BindableProperty<string> password = new BindableProperty<string>();

    public void Update(UserModel userModel)
    {
        this.userName.Value = userModel.Name;
        this.password.Value = userModel.Password;
    }
}
