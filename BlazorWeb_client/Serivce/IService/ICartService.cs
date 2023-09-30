﻿
using BlazorWeb_Client.ViewModels;

namespace BlazorWeb_Client.Serivce.IService
{
    public interface ICartService
    {
        public event Action OnChange;
        Task DecrementCart(ShoppingCart shoppingCart);
        Task IncrementCart(ShoppingCart shoppingCart);
    }
}
