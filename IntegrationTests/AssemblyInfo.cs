[assembly: Parallelizable(ParallelScope.All)]

#if DEBUG
[assembly: LevelOfParallelism(2)]
#else
[assembly: LevelOfParallelism(4)]
#endif