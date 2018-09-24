
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
        public string[] ally;
        public string[] enemy;
    }


    public class MapStructInfo
    {
        virtual public string mapStruct() { return null; }

    }
    
    public class MapPlain: MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"MAP1\"," +
                "\"frame\":\"map_frame\"," +
                "\"ally\":[\"3-4\",\"4-4\",\"10-10\"]," +
                "\"enemy\":[\"4-8-0\",\"5-8-1\",\"10-9-0\"]," +
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
                "\"ally\":[\"3-4\",\"4-4\",\"10-10\"]," +
                "\"enemy\":[\"4-8-0\",\"5-8-1\",\"10-9-0\"]," +
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


    public class MapUranohoshiClub : MapStructInfo
    {
        override public string mapStruct()
        {
            return "{" +
                "\"name\":\"UranohoshiClub\"," +
                "\"frame\":\"map_uranohoshiclub_bg\"," +
                "\"ally\":[\"1-1\",\"4-4\",\"10-10\"]," +
                "\"enemy\":[\"4-8-0\",\"5-8-1\",\"10-9-0\"]," +
                "\"mapstruct\":" +
                "[" +
                "2,2,2,2,2,2," +
                "1,1,2,2,2,2," +
                "1,2,22,2,2,2," +
                "1,21,2,2,2,2," +
                "1,2,1,2,2,2," +
                "1,1,1,2,2,2" +
                "]" +
                "}";
        }
    }

}