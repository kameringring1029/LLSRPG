﻿
/*
 * Map情報の定義
 */

namespace Information
{

    [System.Serializable]
    public class mapinfo
    {
        public string name;
        public string frame;
        public int[] mapstruct;
        public int[] ally1;
        public int[] ally2;
        public int[] ally3;
        public int[] enemy1;
        public int[] enemy2;
        public int[] enemy3;
    }


    public class MapStructInfo
    {
        virtual public string mapStruct() { return null; }

    }

    public class MapStruct1 : MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"MAP1\"," +
                "\"mapstruct\":" +
                "[" +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,2,2,1,4,3,4,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" +
                "]" +
                "}";
        }
    }


    public class MapStruct2 : MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"MAP1\"," +
                "\"frame\":\"map_frame\"," +
                "\"ally1\":[4,4]," +
                "\"ally2\":[5,4]," +
                "\"ally3\":[6,4]," +
                "\"enemy1\":[4,8]," +
                "\"enemy2\":[5,8]," +
                "\"enemy3\":[6,8]," +
                "\"mapstruct\":" +
                "[" +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,2,1,1,1,1,1,1,1,1,1,1," +
                "1,2,2,1,4,3,4,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1" +
                "]" +
                "}";
        }
    }

    public class MapStruct3: MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"MAP1\"," +
                "\"frame\":\"map_jinja\"," +
                "\"ally1\":[4,2]," +
                "\"ally2\":[5,2]," +
                "\"ally3\":[6,2]," +
                "\"enemy1\":[4,8]," +
                "\"enemy2\":[5,8]," +
                "\"enemy3\":[6,8]," +
                "\"mapstruct\":" +
                "[" +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "3,3,3,3,1,1,1,1,3,3,3,3," +
                "1,1,1,3,1,1,1,1,3,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,2,1,1,1,1,1,1,2,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1" +
                "]" +
                "}";
        }
    }

    public class MapPlain: MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"MAP1\"," +
                "\"frame\":\"map_frame\"," +
                "\"ally1\":[4,2]," +
                "\"ally2\":[5,2]," +
                "\"ally3\":[6,2]," +
                "\"enemy1\":[4,8]," +
                "\"enemy2\":[5,8]," +
                "\"enemy3\":[6,8]," +
                "\"mapstruct\":" +
                "[" +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1," +
                "1,1,1,1,1,1,1,1,1,1,1,1" +
                "]" +
                "}";
        }
    }



    public class MapOtonokiProof : MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"OtonokiProof\"," +
                "\"frame\":\"map_otonokiproof\"," +
                "\"ally1\":[3,4]," +
                "\"ally2\":[4,4]," +
                "\"ally3\":[10,10]," +
                "\"enemy1\":[4,8]," +
                "\"enemy2\":[5,8]," +
                "\"enemy3\":[10,9]," +
                "\"mapstruct\":" +
                "[" +
                "1,1,1,1,1,1,1,1,2,2,2,3,3,3," +
                "1,1,1,1,1,1,1,1,2,2,2,3,3,3," +
                "1,1,1,1,1,1,1,1,2,2,2,3,3,3," +
                "1,1,1,1,1,1,1,1,2,2,2,2,3,3," +
                "1,1,1,1,1,1,1,1,2,2,2,2,3,3," +
                "1,112,1,1,1,1,1,1,11,2,2,2,3,3," +
                "1,1,1,1,1,1,1,1,1,1,2,2,3,3," +
                "1,1,1,1,1,1,1,1,1,1,2,3,3,3," +
                "1,1,1,1,1,1,1,1,1,1,2,12,3,3," +
                "1,1,1,111,1,1,1,1,1,1,1,1,2,3," +
                "1,1,1,1,1,1,1,1,1,1,1,1,2,3," +
                "1,1,1,1,1,1,1,1,1,1,1,1,2,3," +
                "1,1,1,1,1,1,1,1,1,1,2,2,3,3," +
                "1,1,1,1,1,1,1,1,1,1,13,2,3,3" +
                "]" +
                "}";
        }
    }
}