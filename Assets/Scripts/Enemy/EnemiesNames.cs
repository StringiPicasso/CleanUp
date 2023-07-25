using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesNames : MonoBehaviour
{
    private List<string> _names=new List<string> 
    {"Aron","Baekhyun","BamBam","Baro","Bobby","Chan","Changmin","Choiza","Daesung","David","Gaho","Gary",
        "Jackson","Jaehyun","Minho","Jungkook","Justin","Daniel","Minhyuk","Kasper","Kevin","Kino","Taemin","Lucas",
        "Mark","Chanyeol","PSY","Rain","Rocky","Samuel","Seungri","Sleepy","Suho","Tablo","Taeyang","Teddy","Woody","Zico",
        "AleXa","Arin","Suzy","BoA","Dana","Dawon","Gummy","Hana","Jennie","Jessi","Mina","Lexie","Lisa","Minnie","Nada","Raina","Sunny","Wendy","Lolas"};

   
    private string _nameEnemy;
    private int _randomIndex;
    public string NameEnemy => _nameEnemy;

    private void AddName()
    {
        _names.Add("Aron"); _names.Add("Baekhyun"); _names.Add("BamBam"); _names.Add("Baro"); _names.Add("Bobby");
        _names.Add("Chan"); _names.Add("Changmin"); _names.Add("Choiza"); _names.Add("Daesung"); _names.Add("David");
        _names.Add("Gaho"); _names.Add("Gary"); _names.Add("Jackson"); _names.Add("Jaehyun"); _names.Add("Minho");
        _names.Add("Jungkook"); _names.Add("Justin"); _names.Add("Daniel"); _names.Add("Minhyuk"); _names.Add("Kasper");
        _names.Add("Kevin"); _names.Add("Kino"); _names.Add("Taemin"); _names.Add("Lucas"); _names.Add("Mark");
        _names.Add("Chanyeol"); _names.Add("PSY"); _names.Add("Rain"); _names.Add("Rocky"); _names.Add("Samuel");
        _names.Add("Seungri"); _names.Add("Sleepy"); _names.Add("Suho"); _names.Add("Tablo"); _names.Add("Taeyang");
        _names.Add("Teddy"); _names.Add("Woody"); _names.Add("Zico"); _names.Add("AleXa"); _names.Add("Arin");
        _names.Add("Suzy"); _names.Add("BoA"); _names.Add("Dana"); _names.Add("Dawon"); _names.Add("Gummy"); _names.Add("Hana"); _names.Add("Jennie");
        _names.Add("Jessi"); _names.Add("Mina"); _names.Add("Lexie"); _names.Add("Lisa"); _names.Add("Minnie"); _names.Add("Nada"); _names.Add("Raina");
        _names.Add("Sunny"); _names.Add("Wendy");
    }

    public string ChangeName()
    {
        _randomIndex = Random.Range(0, _names.Count);
        _nameEnemy = _names[_randomIndex];
        _names.RemoveAt(_randomIndex);

        if (_names.Count < 2)
        {
            AddName();
        }

        return NameEnemy;
    }
}
