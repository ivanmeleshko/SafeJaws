
using System;

namespace Assets.Scripts.SerializedObject
{
    [Serializable]
    public class UserInfoItem
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public UserInfoItem() { }

        public UserInfoItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}