﻿namespace EasyDapper.Data.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
