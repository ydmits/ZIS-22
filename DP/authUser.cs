using System;

namespace KP_Mitsura
{
    public static class authUser
    {
        private static String login;
        private static Int32 mode;
        private static Int32 id;
        private static String name;
        private static String surName;
        private static String midName;
        public static String Login
        { 
            get { return login; } 
            set { login = value; } 
        }
        public static Int32 Mode 
            { 
            get { return mode; } 
            set { mode = value; } 
        }
        public static Int32 Id
        {
            get { return id; }
            set { id = value; }
        }
        public static String Name
        {
            get { return name; }
            set { name = value; }
        }
        public static String SurName
        {
            get { return surName; }
            set { surName = value; }
        }
        public static String MidName
        {
            get { return midName; }
            set { midName = value; }
        }
        public static String getFIO()
        {
            return SurName + " " + Name + " " + MidName;
        }
    }
}
