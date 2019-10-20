
/*
 * 収穫物とかのステータス用クラス
 */

public class NogyoItemStatus
{
    int sweet;
    int bitter;
    int sour;
    int salty;
    int umami;

    public NogyoItemStatus(int sw =0, int bi =0, int so =0, int sa =0, int um =0)
    {
        sweet = sw;
        bitter = bi;
        sour = so;
        salty = sa;
        umami = um;
    }
}
