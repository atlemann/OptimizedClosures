open BenchmarkDotNet.Attributes;
open BenchmarkDotNet.Running

module Foo =

    let bar (f : 'a -> 'a -> 'a) (a:'a array) (b:'a array) =
        Array.map2 f a b

    let barInPlace (f : 'a -> 'a -> 'a) (a:'a array) (b:'a array) =
        for i in 0..b.Length - 1 do
            b.[i] <- f a.[i] b.[i]

    let inline barInPlaceInline (f : 'a -> 'a -> 'a) (a:'a array) (b:'a array) =
        for i in 0..b.Length - 1 do
            b.[i] <- f a.[i] b.[i]

    let barInPlaceOptClosure (f : 'a -> 'a -> 'a) (a:'a array) (b:'a array) =
        let func = OptimizedClosures.FSharpFunc<'a, 'a, 'a>.Adapt(f)
        for i in 0..b.Length - 1 do
            b.[i] <- func.Invoke(a.[i], b.[i])

    let inline barInPlaceOptClosureInline (f : 'a -> 'a -> 'a) (a:'a array) (b:'a array) =
        let func = OptimizedClosures.FSharpFunc<'a, 'a, 'a>.Adapt(f)
        for i in 0..b.Length - 1 do
            b.[i] <- func.Invoke(a.[i], b.[i])

    let barFunInside (a:single array) (b:single array) =
        let f = (fun a b -> a * 0.7f + b * 0.3f)
        for i in 0..b.Length - 1 do
            b.[i] <- f a.[i] b.[i]    

[<MemoryDiagnoser>]
type FooBenchmarks() =
    let mutable a : single array = [||]
    let mutable b : single array = [||]
    
    [<Params (100, 200)>]
    member val public N = 0 with get, set
    
    [<GlobalSetup>]
    member self.GlobalSetupData() =
        let size = self.N * self.N * self.N
        a <- Array.create size 0.3f
        b <- Array.create size 0.42f
        
    [<Benchmark>]
    member self.Bar () =
        Foo.bar (fun a b -> a * 0.7f + b * 0.3f) a b
        
    [<Benchmark>]
    member self.BarInPlace () =
        Foo.barInPlace (fun a b -> a * 0.7f + b * 0.3f) a b

    [<Benchmark>]
    member self.BarInPlaceInline () =
        Foo.barInPlaceInline (fun a b -> a * 0.7f + b * 0.3f) a b

    [<Benchmark>]
    member self.BarInPlaceOptClosure () =
        Foo.barInPlaceOptClosure (fun a b -> a * 0.7f + b * 0.3f) a b

    [<Benchmark>]
    member self.BarInPlaceOptClosureInline () =
        Foo.barInPlaceOptClosureInline (fun a b -> a * 0.7f + b * 0.3f) a b
    
    [<Benchmark>]
    member self.BarFunInside () =
        Foo.barFunInside a b

[<EntryPoint>]
let main argv =
    BenchmarkSwitcher.FromAssembly(typeof<FooBenchmarks>.Assembly).Run(argv)
    |> ignore
    0
