namespace AG.Wpf.NavigationService.UserControlNav
{
    internal class ViewPair
    {
        public string ViewKey { get; }
        public object ViewParameter { get; }

        public ViewPair(string key, object parameter)
        {
            ViewKey = key;
            ViewParameter = parameter;
        }
    }

}
