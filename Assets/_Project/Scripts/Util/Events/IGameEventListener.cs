﻿
namespace Util.Events
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T t);
    }
}