[assembly: Parallelizable(ParallelScope.All)]

#if DEBUG
[assembly: LevelOfParallelism(4)]
#else
[assembly: LevelOfParallelism(4)]
#endif