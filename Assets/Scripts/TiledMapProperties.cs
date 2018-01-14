// TODO: Add your custom map properties as attributes of this class
// Why?  Unity3d's JsonUtility is very lightweight and efficient
// but it does not have the ability to traverse arbitrary json
// therefore you must predefine the custom properties you plan on 
// using as attributes in this class...
namespace TiledJsonUtility
{
    public class MapProperties
    {
        public string mapProperty1;
        public string mapProperty2;
    }
}
