``` ini

BenchmarkDotNet=v0.11.5, OS=ubuntu 18.04
Intel Core i5 CPU 760 2.80GHz (Nehalem), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT DEBUG
  DefaultJob : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT


```
|                     Method |   N |      Mean |     Error |    StdDev |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|--------------------------- |---- |----------:|----------:|----------:|---------:|---------:|---------:|-----------:|
|                        **Bar** | **100** |  **5.781 ms** | **0.1143 ms** | **0.2062 ms** | **234.3750** | **234.3750** | **234.3750** |  **4000048 B** |
|                 BarInPlace | 100 |  7.947 ms | 0.1380 ms | 0.1291 ms |        - |        - |        - |       24 B |
|           BarInPlaceInline | 100 |  1.481 ms | 0.0169 ms | 0.0141 ms |        - |        - |        - |          - |
|       BarInPlaceOptClosure | 100 |  3.687 ms | 0.0385 ms | 0.0360 ms |        - |        - |        - |       24 B |
| BarInPlaceOptClosureInline | 100 |  3.684 ms | 0.0291 ms | 0.0258 ms |        - |        - |        - |       24 B |
|               BarFunInside | 100 |  1.484 ms | 0.0240 ms | 0.0225 ms |        - |        - |        - |          - |
|                        **Bar** | **200** | **47.405 ms** | **0.7069 ms** | **0.6612 ms** |        **-** |        **-** |        **-** | **32000048 B** |
|                 BarInPlace | 200 | 63.368 ms | 0.9844 ms | 0.9208 ms |        - |        - |        - |       24 B |
|           BarInPlaceInline | 200 | 12.653 ms | 0.1514 ms | 0.1416 ms |        - |        - |        - |          - |
|       BarInPlaceOptClosure | 200 | 29.689 ms | 0.3378 ms | 0.3160 ms |        - |        - |        - |       24 B |
| BarInPlaceOptClosureInline | 200 | 29.669 ms | 0.3179 ms | 0.2974 ms |        - |        - |        - |       24 B |
|               BarFunInside | 200 | 12.629 ms | 0.1294 ms | 0.1147 ms |        - |        - |        - |          - |
