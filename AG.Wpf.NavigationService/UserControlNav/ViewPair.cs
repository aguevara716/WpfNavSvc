namespace AG.Wpf.NavigationService.UserControlNav
{
    /// <summary>
    /// For use with the forward and back histories.
    /// Keeps track of the view's key and associated parameter object.
    /// </summary>
    internal class ViewPair
    {
        /// <summary>The key of the view, taken from the nav service's dictionary of views.</summary>
        public string ViewKey { get; }
        /// <summary>The parameter that was originally passed to the view.</summary>
        public object ViewParameter { get; }

        /// <summary>
        /// Creates a new ViewPair, assigning the readonly properties of ViewKey and ViewParameters.
        /// </summary>
        /// <param name="key">The view's key, from the dictionary's key property</param>
        /// <param name="parameter">The view's parameter</param>
        public ViewPair(string key, object parameter)
        {
            ViewKey = key;
            ViewParameter = parameter;
        }
    }

}
