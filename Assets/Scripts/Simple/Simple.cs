//using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Point
{
    public int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator +(Point a, Point b)
    {
        a.x += b.x;
        a.y += b.y;
        return a;
    }

    public static Point operator -(Point a, Point b)
    {
        a.x -= b.x;
        a.y -= b.y;
        return a;
    }

    public float GetMagnitude()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public float GetMagnitudeSqr()
    {
        return x * x + y * y;
    }

    public static Vector2 operator +(Point a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Vector2 operator -(Point a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    public static Vector3 operator +(Point a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y);
    }

    public static Vector3 operator -(Point a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y);
    }

    public static Vector2Int operator +(Point a, Vector2Int b)
    {
        return new Vector2Int(a.x + b.x, a.y + b.y);
    }

    public static Vector2Int operator -(Point a, Vector2Int b)
    {
        return new Vector2Int(a.x - b.x, a.y - b.y);
    }

    public static Vector2Int operator +(Vector2Int a, Point b)
    {
        return new Vector2Int(a.x + b.x, a.y + b.y);
    }

    public static Vector2Int operator -(Vector2Int a, Point b)
    {
        return new Vector2Int(a.x - b.x, a.y - b.y);
    }

    public static Vector3Int operator +(Point a, Vector3Int b)
    {
        return new Vector3Int(a.x + b.x, a.y + b.y, b.z);
    }

    public static Vector3Int operator -(Point a, Vector3Int b)
    {
        return new Vector3Int(a.x - b.x, a.y - b.y, b.z);
    }

    public static Vector3Int operator +(Vector3Int a, Point b)
    {
        return new Vector3Int(a.x + b.x, a.y + b.y, a.z);
    }

    public static Vector3Int operator -(Vector3Int a, Point b)
    {
        return new Vector3Int(a.x - b.x, a.y - b.y, a.z);
    }

    public static Point operator *(Point a, Point b)
    {
        a.x *= b.x;
        a.y *= b.y;
        return a;
    }

    public static Point operator /(Point a, Point b)
    {
        a.x /= b.x;
        a.y /= b.y;
        return a;
    }

    public static Point operator *(Point a, float b)
    {
        a.x = Mathf.RoundToInt(a.x * b);
        a.y = Mathf.RoundToInt(a.y * b);
        return a;
    }

    public static Point operator /(Point a, float b)
    {
        a.x = Mathf.RoundToInt(a.x / b);
        a.y = Mathf.RoundToInt(a.y / b);
        return a;
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return a.x != b.x || a.y != b.y;
    }

    public static implicit operator Vector2(Point point)
    {
        return new Vector2(point.x, point.y);
    }

    public static implicit operator Vector3(Point point)
    {
        return new Vector3(point.x, point.y);
    }

    public static implicit operator Vector2Int(Point point)
    {
        return new Vector2Int(point.x, point.y);
    }

    public static implicit operator Vector3Int(Point point)
    {
        return new Vector3Int(point.x, point.y, 0);
    }

    public static implicit operator Point(Vector2 point)
    {
        return new Point(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y));
    }

    public static implicit operator Point(Vector3 point)
    {
        return new Point(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y));
    }

    public static implicit operator Point(Vector2Int point)
    {
        return new Point(point.x, point.y);
    }

    public static implicit operator Point(Vector3Int point)
    {
        return new Point(point.x, point.y);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }
}

[System.Serializable]
public struct CurveKeyframe
{
    public float time, value, inTangent, outTangent, inWeight, outWeight;
    public WeightedMode WeightedMode;

    public CurveKeyframe(float time, float value)
    {
        this.time = time;
        this.value = value;
        inTangent = 0;
        outTangent = 0;
        inWeight = 0;
        outWeight = 0;
        WeightedMode = WeightedMode.None;
    }

    public CurveKeyframe(float time, float value, float inTangent, float outTangent)
    {
        this.time = time;
        this.value = value;
        this.inTangent = inTangent;
        this.outTangent = outTangent;
        inWeight = 0;
        outWeight = 0;
        WeightedMode = WeightedMode.None;
    }

    public CurveKeyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
    {
        this.time = time;
        this.value = value;
        this.inTangent = inTangent;
        this.outTangent = outTangent;
        this.inWeight = inWeight;
        this.outWeight = outWeight;
        WeightedMode = WeightedMode.None;
    }

    public static implicit operator Keyframe(CurveKeyframe key)
    {
        return new Keyframe(key.time, key.value, key.inTangent, key.outTangent, key.inWeight, key.outWeight);
    }

    public static implicit operator CurveKeyframe(Keyframe key)
    {
        return new CurveKeyframe(key.time, key.value, key.inTangent, key.outTangent, key.inWeight, key.outWeight);
    }
}

[System.Serializable]
public class Curve
{
    public CurveKeyframe[] keys;

    public AnimationCurve AnimationCurve { get { return new AnimationCurve(Keyframes); } }

    public Keyframe[] Keyframes
    {
        get
        {
            Keyframe[] keyframes = new Keyframe[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                keyframes[i] = keys[i];
            return keyframes;
        }
        set
        {
            keys = new CurveKeyframe[value.Length];
            for (int i = 0; i < value.Length; i++)
                keys[i] = value[i];
        }
    }

    public Curve()
    {

    }

    public Curve(Keyframe[] keys)
    {
        Keyframes = keys;
    }

    public Curve(CurveKeyframe[] keys)
    {
        this.keys = keys;
    }

    public float Evaluate(float t)
    {
        return AnimationCurve.Evaluate(t);
    }

    public static implicit operator AnimationCurve(Curve curve)
    {
        return curve.AnimationCurve;
    }

    public static implicit operator Curve(AnimationCurve curve)
    {
        return new Curve(curve.keys);
    }
}

public static class Simple
{
    static MBForDelay mbForDelay;

    public static MBForDelay MbForDelay
    {
        get
        {
            if (mbForDelay == null)
            {
                mbForDelay = (MBForDelay)CreateObject(typeof(MBForDelay));
                mbForDelay.transform.parent = null;
                mbForDelay.name = "MbForDelay";
            }
            return mbForDelay;
        }
    }

    #region Delay

    public static void StartMethodDelay(System.Action onMethode, float delay, bool timeScale = false)
    {
        MbForDelay.StartCoroutine(MethodDelay(onMethode, delay, timeScale));
    }

    static IEnumerator MethodDelay(System.Action onMethode, float delay, bool timeScale = false)
    {
        if (!timeScale)
            while (delay >= 0)
            {
                delay -= Time.unscaledDeltaTime;
                yield return null;
            }
        else yield return new WaitForSeconds(delay);
        onMethode();
    }

    public static void StartMethodDelayFrames(System.Action onMethode, int countFrame)
    {
        MbForDelay.StartCoroutine(MethodDelayFrame(onMethode, countFrame));
    }

    static IEnumerator MethodDelayFrame(System.Action onMethode, int countFrame)
    {
        for (int i = 0; i < countFrame; i++)
            yield return null;
        onMethode();
    }

    #endregion

    #region Alpha

    static IEnumerator SmoothlyAlphaI(SpriteRenderer sr, float targetAlpha, float time)
    {
        Color color = sr.color;
        time = time / Mathf.Abs(color.a - targetAlpha);
        while (color.a != targetAlpha)
        {
            if (targetAlpha > color.a)
                color.a += Time.deltaTime / time;
            else color.a -= Time.deltaTime / time;
            if (Mathf.Abs(targetAlpha - color.a) < Time.deltaTime / time * 2)
                color.a = targetAlpha;
            sr.color = color;
            if (color.a == targetAlpha)
                break;
            yield return null;
        }
    }

    static IEnumerator SmoothlyAlphaI(Graphic graphic, float targetAlpha, float time)
    {
        Color color = graphic.color;
        while (color.a != targetAlpha)
        {
            color = graphic.color;
            float deltaTime = Time.unscaledDeltaTime;
            if (Mathf.Abs(targetAlpha - color.a) < deltaTime / time)
                color.a = targetAlpha;
            else if (targetAlpha > color.a)
                color.a += deltaTime / time;
            else color.a -= deltaTime / time;
            graphic.color = color;
            if (color.a == targetAlpha)
                break;
            yield return null;
        }
    }

    static IEnumerator BlinkingAlphaI(Graphic im, bool up, float time, float alpha, float offset, AnimationCurve curve)
    {
        if (offset != 0)
            yield return new WaitForSecondsRealtime(offset);
        Color color = im.color;
        float currentTime = 0;
        if (up)
            currentTime = 1;
        while (true)
        {
            currentTime += Time.unscaledDeltaTime / (time * alpha);
            color.a = curve.Evaluate(currentTime) * alpha;
            im.color = color;
            yield return null;
        }
    }

    static IEnumerator BlinkingAlphaI(Graphic graphic, float time = 1, float alpha = 1, float offsetAlpha = 0)
    {
        while (true)
        {
            float value = Time.unscaledTime % time;
            if (value > time * .5f)
                value = time - value;
            value = value / time * alpha * 2 + offsetAlpha;
            Color color = graphic.color;
            float deltaTime = Time.unscaledDeltaTime;
            if (value > color.a)
                color.a += deltaTime / time;
            else color.a -= deltaTime / time;
            if (Mathf.Abs(value - color.a) < deltaTime / time * 2)
                color.a = value;
            graphic.color = color;
            yield return null;
        }
    }

    static IEnumerator BlinkingColorI(Graphic graphic, float time, Color color1, Color color2)
    {
        while (true)
        {
            float value = Time.unscaledTime % time;
            if (value > time * .5f)
                value = time - value;
            value = value / time * 2;
            graphic.color = Color.Lerp(color1, color2, value);
            yield return null;
        }
    }

    #endregion

    #region Color

    static IEnumerator SmoothlyColorI(Graphic graphic, Color color, float time)
    {
        Color startColor = graphic.color;
        float timeValue = 0;
        while (timeValue < time)
        {
            timeValue += Time.deltaTime;
            graphic.color = Color.Lerp(startColor, color, timeValue / time);
            yield return null;
        }
    }

    #endregion

    #region Get

    public static float GetAngle(Vector2 dir)
    {
        dir = dir.normalized;
        if (dir.y == 0)
            if (dir.x >= 0)
                return 0;
            else return 180;
        float angle = Mathf.Asin(dir.y) * 180 / Mathf.PI;
        if (dir.x < 0)
            angle = 180 - angle;
        if (angle < 0)
            angle += 360;
        return angle;
    }

    public static float GetAngle(float x, float y)
    {
        if (y == 0)
            if (x >= 0)
                return 0;
            else return 180;
        float angle = Mathf.Asin(y / new Vector2(x, y).magnitude) * 180 / Mathf.PI;
        if (x < 0)
            angle = 180 - angle;
        if (angle < 0)
            angle += 360;
        return angle;
    }

    public static Vector2 GetVector2Angle(float angle)
    {
        float angleDeg = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleDeg), Mathf.Sin(angleDeg));
    }

    public static void GetMouseClickCell(out int x, out int y, float sizeCell)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            x = (int)((hit.point.x) / sizeCell);
            y = (int)((hit.point.y) / sizeCell);
        }
        else
        {
            x = -1;
            y = -1;
        }
    }

    public static void GetMouseClickPoint(out float x, out float y, float sizeStep)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            x = (int)(hit.point.x / sizeStep) * sizeStep;
            y = (int)(hit.point.y / sizeStep) * sizeStep;
        }
        else
        {
            x = -1;
            y = -1;
        }
    }

    public static Vector3 GetHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            return hit.point;
        return Vector3.zero;
    }

    public static Vector2 GetSizeCollider(Transform tr)
    {
        float minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
        foreach (Collider2D col in tr.GetComponentsInChildren<Collider2D>())
        {
            if (!col.enabled || col.gameObject.layer != tr.gameObject.layer)
                continue;
            float width = col.bounds.size.x;
            float height = col.bounds.size.y;
            float x = col.bounds.min.x;
            float y = col.bounds.min.y;
            if (x < minX)
                minX = x;
            if (y < minY)
                minY = y;
            if (x + width > maxX)
                maxX = x + width;
            if (y + height > maxY)
                maxY = y + height;
        }
        return new Vector2(maxX - minX, maxY - minY);
    }

    public static List<Transform> GetAllChilds(Transform parent)
    {
        List<Transform> childs = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
            childs.Add(parent.GetChild(i));
        for (int i = 0; i < childs.Count; i++)
            for (int j = 0; j < childs[i].childCount; j++)
                if (!childs.Contains(childs[i].GetChild(j)))
                    childs.Add(childs[i].GetChild(j));
        return childs;
    }

    public static Transform GetChildAllHierarchy(Transform parent, string name)
    {
        foreach (Transform tr in GetAllChilds(parent))
            if (tr.name == name)
                return tr;

        return null;
    }

    public static List<T> GetAllChilds<T, U>(Transform parent, out List<U> listIgnore)
    {
        List<Transform> childs = new List<Transform>();
        listIgnore = new List<U>();
        for (int i = 0; i < parent.childCount; i++)
            childs.Add(parent.GetChild(i));
        for (int i = 0; i < childs.Count; i++)
            if (childs[i].GetComponent<U>() == null)
            {
                for (int j = 0; j < childs[i].childCount; j++)
                    if (childs[i].GetChild(j).GetComponent<U>() == null)
                    {
                        if (!childs.Contains(childs[i].GetChild(j)))
                            childs.Add(childs[i].GetChild(j));
                    }
                    else listIgnore.Add(childs[i].GetChild(j).GetComponent<U>());
            }
            else listIgnore.Add(childs[i].GetComponent<U>());
        List<T> rezult = new List<T>();
        foreach (Transform child in childs)
            rezult.Add(child.GetComponent<T>());
        for (int i = 0; i < rezult.Count; i++)
            if (rezult[i].ToString() == "null")
            {
                rezult.RemoveAt(i);
                i--;
            }
        return rezult;
    }

    public static float GetHeightChilds(RectTransform parent)
    {
        float height = 0;
        float min = int.MaxValue;
        foreach (RectTransform rect in parent.GetComponentsInChildren<RectTransform>())
            if (rect != parent)
            {
                height = Mathf.Max(height, -rect.anchoredPosition.y + rect.rect.height);
                min = Mathf.Min(min, -rect.anchoredPosition.y);
            }
        if (min == int.MaxValue)
            return 0;
        else return height - min;
    }

    public static GameObject Instantiate(string name)
    {
        return Object.Instantiate(Resources.Load<GameObject>(name));
    }

    public static string GetText(string key, string category = "")
    {
        string text = "";
        string s = category + "/" + key;
        if (category == "")
            s = key;
        //text = ScriptLocalization.Get(s);
        if (text != null)
            return text;
        Debug.Log("Слово не найдено - " + s);
        return key;
    }

    public static string GetTextTime(float time)
    {
        return string.Format("{0:00}:{1:00}", time / 60, time % 60);
    }

    public static string GetTextIcon(string icon, string atlas = "ResourceIcons", string color = "F2CC0CFF")
    {
        return "<sprite=\"" + atlas + "\" name=\"" + icon + "\" color=#" + color + ">";
    }

    #endregion

    #region Folder

    public static void CreatePathFolder(string root, List<string> sub)
    {
        if (sub == null || sub.Count == 0)
            return;

        DirectoryInfo dirInfo = new DirectoryInfo(root);
        if (sub[0] == root)
            sub.RemoveAt(0);
        string path = root + "/" + sub[0];

        if (string.IsNullOrEmpty(Path.GetExtension(sub[0])))
        {
            if (!Directory.Exists(path))
                dirInfo.CreateSubdirectory(sub[0]);

            sub.RemoveAt(0);
            CreatePathFolder(path, sub);
        }
        else
        {
            if (!File.Exists(path))
                File.Create(path).Close();
        }
    }

    public static void DeleteFolder(string path)
    {
        if (!Directory.Exists(path))
            return;
        List<string> dirs = new List<string>(Directory.GetDirectories(path, "*", SearchOption.AllDirectories));
        dirs.Reverse();
        dirs.Add(path);
        foreach (string dir in dirs)
        {
            foreach (string file in Directory.GetFiles(dir))
                File.Delete(file);
            Directory.Delete(dir);
        }
        File.Delete(path + ".meta");
    }

    #endregion

    #region Analytic

    public static void AnalyticEvent(string name)
    {
        if (Application.isEditor)
            return;
        //Analytics.CustomEvent(name);
        //AppMetrica.Instance.ReportEvent(name);
    }

    public static void AnalyticEvent(string name, params object[] dic)
    {
        if (Application.isEditor)
            return;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        for (int i = 0; i < dic.Length - 1; i += 2)
            dictionary.Add(dic[i].ToString(), dic[i + 1]);
        //Analytics.CustomEvent(name, dictionary);
        //AppMetrica.Instance.ReportEvent(name, dictionary);
    }

    public static void AnalyticEvent(string name, Dictionary<string, object> dictionary)
    {
        if (Application.isEditor)
            return;
        //Analytics.CustomEvent(name, dictionary);
        //AppMetrica.Instance.ReportEvent(name, dictionary);
    }

    #endregion

    #region Comparer

    public static int ComparerStringLenght(string a, string b)
    {
        if (a.Length > b.Length)
            return -1;
        else if (a.Length < b.Length)
            return 1;
        else return 0;
    }

    #endregion

    #region Кривая Бизье
    /// <summary>Построение кривой Бизье</summary>
    /// <param name="P">Массив точек</param>
    /// <param name="length">Кол-во шагов на всю кривую</param>
    /// <returns>Возвращает массив (размером length) точек</returns>
    public static List<Vector3> CurveBezie(List<Vector3> P, int length)
    {
        List<Vector3> curve = new List<Vector3>();
        float t;
        Vector3 s = new Vector3();
        for (int j = 0; j < length + 1; j++)
        {
            t = (float)j / length;
            for (int i = 0; i < P.Count; i++)
                s += P[i] * B(i, P.Count - 1, t);
            curve.Add(s);
            s = new Vector3();
        }
        return curve;
    }

    /// <summary>Построение кривой Бизье</summary>
    /// <param name="P">Массив точек</param>
    /// <param name="length">Кол-во шагов на всю кривую</param>
    /// <returns>Возвращает массив (размером length) точек</returns>
    public static List<Vector2> CurveBezie(List<Vector2> P, int length)
    {
        List<Vector2> curve = new List<Vector2>();
        float t;
        Vector2 s = new Vector2();
        for (int j = 0; j < length + 1; j++)
        {
            t = (float)j / length;
            for (int i = 0; i < P.Count; i++)
                s += P[i] * B(i, P.Count - 1, t);
            curve.Add(s);
            s = new Vector2();
        }
        return curve;
    }

    /// <summary>Построение кривой Бизье</summary>
    /// <param name="P">Массив точек</param>
    /// <param name="length">Кол-во шагов на всю кривую</param>
    /// <returns>Возвращает массив (размером length) точек</returns>
    public static List<Vector2> CurveBezie(int length, params Vector2[] P)
    {
        return CurveBezie(new List<Vector2>(P), length);
    }

    public static Vector2 CurveBeziePoint(List<Vector2> P, float t)
    {
        Vector2 point = Vector2.zero;
        for (int i = 0; i < P.Count; i++)
            point += P[i] * B(i, P.Count - 1, t);
        return point;
    }

    static float B(int i, int n, float t)
    {
        return Factorial(n) / (Factorial(i) * (Factorial(n - i))) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }

    static float Factorial(int f)
    {
        float s = 1;
        for (int i = 1; i < f + 1; i++)
            s *= i;
        return s;
    }
    #endregion

    #region Методы расширения

    public static Vector2 ToVector2(this string text)
    {
        Vector2 rezult = new Vector2();
        string[] xy = text.Split(new string[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries);
        rezult.x = float.Parse(xy[0].Remove(0, 1));
        rezult.y = float.Parse(xy[1].Remove(xy[1].Length - 1, 1));
        return rezult;
    }

    public static Vector3 ToVector3(this string text)
    {
        Vector3 rezult = new Vector3();
        string[] xyz = text.Split(new string[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries);
        rezult.x = float.Parse(xyz[0].Remove(0, 1));
        rezult.y = float.Parse(xyz[1]);
        rezult.z = float.Parse(xyz[2].Remove(xyz[2].Length - 1));
        return rezult;
    }

    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }

    public static Vector2Int ToVector2Int(this Vector2 vector2)
    {
        return new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
    }

    public static Vector2Int ToVector2Int(this Vector3 vector3)
    {
        return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
    }

    public static Vector3Int ToVector3Int(this Vector2 vector2)
    {
        return new Vector3Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y), 0);
    }

    public static Vector3Int ToVector3Int(this Vector3 vector3)
    {
        return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
    }

    public static Vector2Int ToVector2Int(this Vector3Int vector)
    {
        return new Vector2Int(vector.x, vector.y);
    }

    public static Vector3Int ToVector3Int(this Vector2Int vector)
    {
        return new Vector3Int(vector.x, vector.y, 0);
    }

    public static int ToInt(this bool value)
    {
        return value ? 1 : 0;
    }

    public static bool ToBool(this int value)
    {
        return value == 0 ? false : true;
    }

    static public bool CheckContainsPoint(this List<Vector3> points, Vector3 target)
    {
        for (int i = 0; i < points.Count; i++)
            if ((target - points[i]).sqrMagnitude < 5000)
                return true;
        return false;
    }

    //public static Vector2 Cell(this Vector2 vector2, int sizeCell = Data.sizeCell)
    //{
    //    return new Vector2(Mathf.Round(vector2.x / sizeCell), Mathf.Round(vector2.y / sizeCell));
    //}

    //public static Vector3 Cell(this Vector3 vector3, int sizeCell = Data.sizeCell)
    //{
    //    return new Vector3(Mathf.Round(vector3.x / sizeCell), Mathf.Round(vector3.y / sizeCell));
    //}

    //public static Vector2Int CellInt(this Vector2 vector2, int sizeCell = Data.sizeCell)
    //{
    //    return new Vector2Int(Mathf.RoundToInt(vector2.x / sizeCell), Mathf.RoundToInt(vector2.y / sizeCell));
    //}

    ////public static Vector2Int CellInt(this Vector3 vector3, float sizeCell = Data.sizeCell)
    ////{
    ////    return new Vector2Int(Mathf.RoundToInt(vector3.x / sizeCell), Mathf.RoundToInt(vector3.y / sizeCell));
    ////}

    //public static Vector3Int CellInt(this Vector3 vector3, float sizeCell = Data.sizeCell)
    //{
    //    return new Vector3Int(Mathf.RoundToInt(vector3.x / sizeCell), Mathf.RoundToInt(vector3.y / sizeCell), (int)vector3.z);
    //}

    //public static Vector2 RoundToCell(this Vector2 vector2, int sizeCell = Data.sizeCell)
    //{
    //    return new Vector2(RoundToCell(vector2.x, sizeCell), RoundToCell(vector2.y, sizeCell));
    //}

    //public static Vector3 RoundToCell(this Vector3 vector3, float sizeCell = Data.sizeCell)
    //{
    //    return new Vector3(RoundToCell(vector3.x, sizeCell), RoundToCell(vector3.y, sizeCell), RoundToCell(vector3.z, sizeCell));
    //}

    public static Vector2 ClampDev(this Vector2 vector2, int size)
    {
        return new Vector2(vector2.x % size, vector2.y % size);
    }

    public static Vector2 Multiply(this Vector2 original, Vector2 multiplier)
    {
        original.x *= multiplier.x;
        original.y *= multiplier.y;
        return original;
    }

    public static Vector2 Division(this Vector2 original, Vector2 multiplier)
    {
        original.x /= multiplier.x;
        original.y /= multiplier.y;
        return original;
    }

    public static string ToText(this Vector2 value)
    {
        return "{" + value.x + ", " + value.y + "}";
    }

    public static float GetRandomInterval(this Vector2 value)
    {
        return Random.Range(value.x, value.y);
    }

    public static void SetAlpha(this SpriteRenderer sr, float a)
    {
        if (sr == null)
            return;
        Color color = sr.color;
        color.a = a;
        sr.color = color;
    }

    public static void SetAlpha(this Graphic image, float a)
    {
        if (image == null)
            return;
        image.StopAllCoroutines();
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
    }

    public static void SetAlpha(this Text text, float a)
    {
        if (text == null)
            return;
        text.StopAllCoroutines();
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
    }

    public static void SmoothlyAlpha(this SpriteRenderer sr, float targetAlpha, float time = 0.3f)
    {
        if (sr == null || sr.color.a == targetAlpha)
            return;
        MbForDelay.StartCoroutine(SmoothlyAlphaI(sr, targetAlpha, time));
    }

    public static void SmoothlyAlpha(this Graphic image, float a, float time = 0.3f)
    {
        if (image == null || !image.gameObject.activeInHierarchy)
            return;
        if (image.color.a == a)
            return;
        image.StopAllCoroutines();
        image.StartCoroutine(SmoothlyAlphaI(image, a, time));
    }

    public static void BlinkingAlpha(this Graphic image, bool up, AnimationCurve curve, float time = 0.5f, float alpha = 1, float offset = 0)
    {
        if (image == null || !image.gameObject.activeInHierarchy)
            return;
        image.StopAllCoroutines();
        image.StartCoroutine(BlinkingAlphaI(image, up, time, alpha, offset, curve));
    }

    public static void BlinkingAlpha(this Graphic image, float time = 1, float alpha = 1, float offsetAlpha = 0)
    {
        if (image == null || !image.gameObject.activeInHierarchy)
            return;
        image.StopAllCoroutines();
        image.StartCoroutine(BlinkingAlphaI(image, time, alpha, offsetAlpha));
    }

    public static void BlinkingColor(this Graphic image, Color color1, Color color2, float time = .5f)
    {
        if (image == null || !image.gameObject.activeInHierarchy)
            return;
        image.StopAllCoroutines();
        image.StartCoroutine(BlinkingColorI(image, time, color1, color2));
    }

    public static void SmoothlyColor(this Graphic image, Color color, float a = -1, float time = 0.3f)
    {
        if (image == null || !image.gameObject.activeInHierarchy)
            return;
        if (a != -1)
            color.a = a;
        if (image.color == color)
            return;
        image.StopAllCoroutines();
        image.StartCoroutine(SmoothlyColorI(image, color, time));
    }
    #endregion

    #region Other

    public static void MethodTimeImplementation(System.Action action)
    {
        System.Diagnostics.Stopwatch time = System.Diagnostics.Stopwatch.StartNew();
        action?.Invoke();
        Debug.Log(time.Elapsed.TotalMilliseconds);
    }

    public static float RoundToCell(float value, float sizeCell)
    {
        if (value == 0)
            return 0;
        return Mathf.Round(value / sizeCell) * sizeCell;
    }

    public static bool SameSign(float a, float b)
    {
        if (a < 0 && b < 0)
            return true;
        else if (a > 0 && b > 0)
            return true;
        else return false;
    }

    public static void SetLanguage(string language)
    {
        //LocalizationManager.CurrentLanguageCode = language;
    }

    public static void ClampText(Text text)
    {
        float board = text.GetComponent<RectTransform>().sizeDelta.x;
        if (text.preferredWidth < board)
            return;
        int countRemoveSymbol = 1;
        while (text.preferredWidth > board)
        {
            text.text = text.text.Remove(text.text.Length - countRemoveSymbol);
            text.text += "...";
            countRemoveSymbol = 4;
        }
    }

    public static void LoadTextureFromByte(Texture2D texture, byte[] data)
    {
        Color32[] colors = new Color32[data.Length / 4];
        int k = 0;
        for (int y = texture.height - 1; y >= 0; --y)
        {
            for (int x = 0; x < texture.width * 4; x += 4)
            {
                int n = x + y * texture.width * 4;
                Color32 c = new Color32(1, 1, 1, 1)
                {
                    r = data[n++],
                    g = data[n++],
                    b = data[n++],
                    a = data[n++]
                };
                colors[k++] = c;
            }
        }

        texture.SetPixels32(colors);
        texture.Apply();
    }

    public static bool EnumTryParse(string value, ref System.Enum e)
    {
        foreach (string name in System.Enum.GetNames(e.GetType()))
            if (name == value)
            {
                e = (System.Enum)System.Enum.Parse(e.GetType(), value);
                return true;
            }
        return false;
    }

    public static Component CreateObject(System.Type type, string name = "NewObject")
    {
        if (type == typeof(Transform) || type == typeof(GameObject))
            return new GameObject(name).transform;
        else return new GameObject(name).AddComponent(type);
    }

    public static Component CreateObject(System.Type type, Transform parent, string name = "NewObject")
    {
        Component component;
        if (type == typeof(Transform) || type == typeof(GameObject))
            component = new GameObject(name).transform;
        else component = new GameObject(name).AddComponent(type);
        component.transform.SetParent(parent);
        return component;
    }

    public static float FloatToTarget(float value, float target, float maxStep)
    {
        if (value < target)
            if (value + maxStep > target)
                return target;
            else return value + maxStep;
        else if (value > target)
            if (value - maxStep < target)
                return target;
            else return value - maxStep;
        else return target;
    }

    public static void Serialize(object obj, string path)
    {
        XmlSerializer formatter = new XmlSerializer(obj.GetType());
        FileStream fs = new FileStream(path, FileMode.Create);
        try { formatter.Serialize(fs, obj); }
        catch { Debug.LogError("Dont save"); }
        fs.Close();
    }

    public static T Deserialize<T>(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        XmlSerializer formatter = new XmlSerializer(typeof(T));
        try
        {
            object obj = formatter.Deserialize(fs);
            fs.Close();
            return (T)obj;
        }
        catch (System.Exception e)
        {
            fs.Close();
            Debug.LogError(e);
        }
        return default(T);
    }

    public static void SerializeBin(object obj, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);
        try { formatter.Serialize(fs, obj); }
        catch { Debug.LogError("Dont save"); }
        fs.Close();
    }

    public static T DeserializeBin<T>(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            object obj = formatter.Deserialize(fs);
            fs.Close();
            return (T)obj;
        }
        catch (System.Exception e)
        {
            fs.Close();
            Debug.LogError(e);
        }
        return default(T);
    }

    #endregion
}