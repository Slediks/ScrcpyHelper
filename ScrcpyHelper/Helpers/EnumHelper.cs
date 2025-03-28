using System.ComponentModel;

namespace ScrcpyHelper.Helpers;

public static class EnumHelper
{
    public static string GetDescription(Enum enumValue)
    {
        var type = enumValue.GetType();
        var memberInfo = type.GetMember(enumValue.ToString());

        if (memberInfo.Length > 0)
        {
            var attributes = (DescriptionAttribute[])memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
        }

        return enumValue.ToString();
    }
}