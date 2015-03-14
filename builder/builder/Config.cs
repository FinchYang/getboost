﻿using builder.Codeplex;

namespace builder
{
    static class Config
    {
        public static readonly Version Version = 
            new UnstableVersion(1, 58, 0, "b1rc2");

        public static List Fix(string text, int issue)
        {
            var s = issue.ToString();
            return T.List
                [T.Text(text)]
                [T.A(s, "https://svn.boost.org/trac/boost/ticket/" + s)];
        }

        public static A GitHub(string id)
        {
            return T.A(
                id, "https://github.com/sergey-shandar/boost/commit/" + id);
        }

        public static readonly List[] Release =
        {
            /*
            T.List
                [T.Text("build from Git ")]
                [GitHub("487b8a11d25b13582460c955ab024f260a282c8a")],
            T.List[T.Text("Boost.Build fix to work with Git")],
            Fix("Boost.Thread fix for ", 9333),
            Fix("Boost.Intrusive fix for ", 9332),
            T.List[T.Text("Boost.Log fix for dump manipulator on AVX2 CPUs")],
            Fix("Boost.Mpi fix for ", 9395),
            Fix("Boost.Serialization fix for ", 9196),
             * */
        };

        public const string Authors = "Sergey Shandar, Boost";

        public const string Owners = "sergey_shandar";

        public const string BoostDir = @"..\..\..\..\..\boost\";

        public static readonly Platform[] PlatformList =
        {
            new Platform("Win32", @"address-model-32\lib"),
            new Platform("x64", @"address-model-64\lib")
        };

        public static readonly Library[] LibraryList =
        {
            // chrono depends on system
            // context needs ASM.
            new Library(
                name: "context",
                packageList: new[]
                {
                    new Package(
                        skip: true
                    )
                }
            ),
            // coroutine
            new Library(
                name: "coroutine",
                packageList: new[]
                {
                    // coroutine
                    new Package(
                        preprocessorDefinitions: new[] 
                        {
                            "BOOST_COROUTINES_NO_LIB"
                        }
                    ),
                    // coroutine_segmented (GCC only)
                    new Package(
                        name: "segmented",
                        fileList: new[] 
                        {
                            @"detail\segmented_stack_allocator.cpp" 
                        },
                        skip: true
                    ),
                    // coroutine_posix (need Posix library).
                    new Package(
                        name: "posix",
                        fileList: new[]
                        {
                            @"detail\standard_stack_allocator_posix.cpp",
                        },
                        skip: true
                    ),
                }),
            // filesystem depends on system.
            // graph depens on regex.
            // graph_parallel depends on mpi.
            // iostreams
            new Library(
                name: "iostreams",
                packageList: new[]
                {
                    new Package(),
                    // need bzip2 lib.
                    new Package(
                        name: "bzip2", 
                        fileList: new[] { "bzip2.cpp" }),
                    // need zlib lib.
                    new Package(
                        name: "zlib",
                        fileList: new[] { "zlib.cpp", "gzip.cpp" }),
                }),
            // locale. WIP.
            new Library(
                name: "locale",
                packageList: new[]
                {
                    // locale, depends on thread, system, date_time, chrono.
                    new Package(
                        lineList: new[] 
                        { 
                            "#define BOOST_LOCALE_NO_POSIX_BACKEND" 
                        },
                        fileList: new[]
                        {
                            @"util\locale_data.hpp",
                            @"shared\mo_hash.hpp",
                            @"encoding\conv.hpp"
                        }
                    ),
                    // locale_posix
                    new Package(
                        name: "posix",
                        fileList: new[]
                        {
                            "posix",
                        },
                        skip: true
                    ),
                    // locale_icu
                    new Package(
                        name: "icu",
                        fileList: new[]
                        {
                            "icu",
                            @"util\locale_data.hpp",
                            @"shared\mo_hash.hpp",
                            @"encoding\conv.hpp"
                        }
                    ),
                }),
            // log
            new Library(
                name: "log",
                packageList: new[]
                {
                    // log depends on system, file_system, date_time, thread, chrono.
                    new Package(
                        lineList: new[] 
                        {
                            "#define BOOST_SPIRIT_USE_PHOENIX_V3"
                        },
                        fileList: new[]
                        {
                            "spirit_encoding.hpp",
                            "windows_version.hpp",
                        }),
                    new Package(
                        name: "event",
                        fileList: new[]
                        {
                            "event_log_backend.cpp"
                        },
                        skip: true
                    ),
                    // need log_event
                    new Package(
                        name: "setup",
                        lineList: new[]
                        {
                            "#define BOOST_SPIRIT_USE_PHOENIX_V3"
                        },
                        fileList: new[]
                        {
                            "default_filter_factory.hpp",
                            "windows_version.hpp",
                            "spirit_encoding.hpp",
                            "parser_utils.hpp",
                            "parser_utils.cpp",
                            "init_from_stream.cpp",
                            "init_from_settings.cpp",
                            "settings_parser.cpp",
                            "filter_parser.cpp",
                            "formatter_parser.cpp",
                            "default_filter_factory.cpp",
                            "matches_relation_factory.cpp",
                            "default_formatter_factory.cpp"
                        },
                        skip: true
                    ),
                }),
            // mpi depends on serialization and MPI (3rd party).
            new Library(
                name: "mpi",
                packageList: new[]
                {
                    // mpi
                    new Package(),
                    // mpi_python
                    new Package(
                        name: "python",
                        fileList: new[]
                        {
                            "python"
                        }),
                }),
            // python
            new Library(
                name: "python",
                packageList: new[]
                {
                    new Package(
                        preprocessorDefinitions: 
                            new[] { "BOOST_PYTHON_STATIC_LIB" },
                        lineList: 
                            new[] { "#define BOOST_PYTHON_SOURCE" }
                    )
                }
            ),
            // regex.
            // serialization.
            // test
            new Library(
                name: "test",
                packageList: new[]
                {
                    // test
                    new Package(
                        skip: true
                    ),
                    // test_cpp_main
                    new Package(
                        name: "cpp_main",
                        fileList: new[]
                        {
                            "cpp_main.cpp"
                        },
                        skip: true
                    ),
                    // test_test_main
                    new Package(
                        name: "test_main",
                        fileList: new[]
                        {
                            "test_main.cpp"
                        },
                        skip: true
                    ),
                    // test_unit_test_main
                    new Package(
                        name: "unit_test_main",
                        fileList: new[]
                        {
                            "unit_test_main.cpp"
                        },
                        skip: true
                    )
                }
            ),
            // thread.
            new Library(
                name: "thread",
                packageList: new[]
                {
                    // thread, depends on date_time, system, chrono.
                    new Package(
                        lineList: new[]
                        {
                            "#define BOOST_HAS_WINTHREADS",
                            "#define BOOST_THREAD_BUILD_LIB",
                        }
                    ),
                    // thread_pthread
                    new Package(
                        name: "pthread",
                        fileList: new[]
                        {
                            "pthread"
                        },
                        skip: true
                    ),
                }
            ),
        };

    }
}
