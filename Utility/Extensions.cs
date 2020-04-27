namespace UnityEngine {
    public static class Extensions
    {
        public static T FindChildWithName<T>(this GameObject gameObject, string name) where T: Component
        {
            foreach (T child in gameObject.GetComponentsInChildren<T>())
            {
                if (child.name.Equals(name))
                {
                    return child;
                }
            }
            return null;
        }

    }
}
