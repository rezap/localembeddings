using LLama;
using LLama.Common;
using LLama.Native;
using System.Numerics;

namespace LocalEmbedding
{
    class Program
    {
        static void Main()
        {
            string modelPath = @"/Users/rezaparseh/Aize/Source/localembeddings/miniLM-l6-v2/ggml-model-f16.gguf";
            var @params = new ModelParams(modelPath) { Embeddings = true};
            using var weights = LLamaWeights.LoadFromFile(@params);
            var embedder = new LLamaEmbedder(weights, @params);
            string[] text = { "boy", "girl" };
            float[][] result = new float[2][];
            for (int index = 0; index < text.Length; index++)
            {
                float[] embeddings = embedder.GetEmbeddings(text[index]).Result;
                result[index] = embeddings;
                Console.WriteLine($"Embeddings contain {embeddings.Length:N0} floating point values:");
                Console.WriteLine(string.Join(", ", embeddings.Take(10)) + ", ...");                
            }
            
            float dot_prod = 0.0F;
            for (int index= 0; index < result[0].Length; index++)
            {
                dot_prod += (result[0][index] * result[1][index]);
            }
            Console.WriteLine(dot_prod);
            int decimalPlaces = 4;
            float expectedDotProd = 0.6163F;
            var tolerance = (float)Math.Pow(10, -decimalPlaces);
            Console.WriteLine((expectedDotProd  - dot_prod) < tolerance); // for ["boy", "girl"]
        }
    }
}