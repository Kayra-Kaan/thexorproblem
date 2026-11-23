using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    ANN ann;
    double sumSquareError = 0;

    public int epochs = 5000;
    public double learningRate = 0.8;

    void Start()
    {
        ann = new ANN(2, 1, 1, 2, learningRate);

        List<double> result;

        for (int i = 0; i < epochs; i++)
        {
            sumSquareError = 0;

            result = Train(1, 1, 0);
            sumSquareError += Mathf.Pow((float)result[0] - 0, 2);

            result = Train(1, 0, 1);
            sumSquareError += Mathf.Pow((float)result[0] - 1, 2);

            result = Train(0, 1, 1);
            sumSquareError += Mathf.Pow((float)result[0] - 1, 2);

            result = Train(0, 0, 0);
            sumSquareError += Mathf.Pow((float)result[0] - 0, 2);
        }

        Debug.Log("Eğitim Tamamlandı! Son Sum Square Error Değeri: " + sumSquareError.ToString("F6"));

        TestXOR(1, 1, 0);
        TestXOR(1, 0, 1);
        TestXOR(0, 1, 1);
        TestXOR(0, 0, 0);
    }

    List<double> Train(double i1, double i2, double o)
    {
        List<double> inputs = new List<double> { i1, i2 };
        List<double> outputs = new List<double> { o };
        return ann.Run(inputs, outputs);
    }

    void TestXOR(double i1, double i2, double expectedOutput)
    {
        List<double> inputs = new List<double> { i1, i2 };
        List<double> desired = new List<double> { expectedOutput };
        List<double> prediction = ann.Run(inputs, desired);
        Debug.Log($"Girdi: ({i1}, {i2}) -> Çıktı: {prediction[0]:F4} (Yuvarlanmış: {Mathf.Round((float)prediction[0])})");
    }
}