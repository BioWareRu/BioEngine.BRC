using System;
using System.Collections.Generic;
using System.Linq;

namespace BioEngine.BRC.Admin.Components
{
    public class MenuBuilder
    {
        private readonly List<MenuBuilderGroup> _groups = new();
        public MenuBuilderGroup[] Groups => _groups.ToArray();

        public MenuBuilderGroup AddGroup(string title)
        {
            var group = new MenuBuilderGroup(title);
            _groups.Add(group);
            return group;
        }
    }

    public record MenuBuilderGroup(string Title)
    {
        private readonly List<MenuBuilderItem> _items = new();
        public MenuBuilderItem[] Items => _items.ToArray();

        public MenuBuilderItem AddItem(string title, string icon, string? link = null)
        {
            var item = new MenuBuilderItem(title, icon, link);
            _items.Add(item);
            return item;
        }
    }


    public record MenuBuilderItem(string Title, string? Icon = null, string? Link = null)
    {
        public Guid Id { get; } = Guid.NewGuid();

        private readonly List<MenuBuilderItem> _children = new();

        public MenuBuilderItem[] Children => _children.ToArray();

        public MenuBuilderItem AddChild(string title, string? link = null, string? icon = null)
        {
            _children.Add(new MenuBuilderItem(title, icon, link));
            return this;
        }

        public bool IsActive(string currentUrl)
        {
            if (Link is not null && currentUrl.StartsWith(Link))
            {
                return true;
            }

            if (_children.Any(c => c.IsActive(currentUrl)))
            {
                return true;
            }

            return false;
        }
    }
}
