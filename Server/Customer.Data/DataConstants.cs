namespace Customer.Data
{
    public class DataConstants
    {
        public class Common
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 100;
            public const int MaxAddressLength = 100;
            public const int MaxUrlLength = 2048;
            public const int Zero = 0;
        }

        public class Categories
        {
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 1000;
        }

        public class Customers
        {
            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 20;
            public const int MaxEmailLength = 50;
            public const string PhoneNumberRegularExpression = @"^[0-9]*";
        }

        public class Products
        {
            public const int MinModelLength = 2;
            public const int MaxModelLength = 100;
        }
    }
}
