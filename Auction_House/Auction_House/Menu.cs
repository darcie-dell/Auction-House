namespace Action_house
{
    public class Menu
    {

        /// <summary>
        /// Class that creates a base menu where items can be displayed and an action selected. 
        /// </summary>
        /// <typeparam name="MenuItem"></typeparam>
        /// <returns></returns>
        private List<MenuItem> items = new List<MenuItem>();

        public void Add(string menuItem, Action eventHandler)
        {
            items.Add(new MenuItem(menuItem, eventHandler));
        }

        public void Display(string title, string prompt)
        {
            DisplayL.DisplayList(title, items, null);
            var option = Validation.getOption(prompt, 1, items.Count);
            items[option].select();
        }


        class MenuItem
        {

            /// <summary>
            /// Menu Item class that creates and object with a title and event to display in a menu.
            /// </summary>

            private string item;

            private Action selected;

            public MenuItem(string item, Action eventHandler)
            {
                this.item = item;
                selected = eventHandler;
            }
            public void select()
            {
                selected();
            }

            public override string ToString()
            {
                return item;
            }

        }


    }
}
