# Generated by CMake

if("${CMAKE_MAJOR_VERSION}.${CMAKE_MINOR_VERSION}" LESS 2.5)
   message(FATAL_ERROR "CMake >= 2.6.0 required")
endif()
cmake_policy(PUSH)
cmake_policy(VERSION 2.6...3.19)
#----------------------------------------------------------------
# Generated CMake target import file.
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Protect against multiple inclusion, which would fail when already imported targets are added once more.
set(_targetsDefined)
set(_targetsNotDefined)
set(_expectedTargets)
foreach(_expectedTarget OpenAL::OpenAL)
  list(APPEND _expectedTargets ${_expectedTarget})
  if(NOT TARGET ${_expectedTarget})
    list(APPEND _targetsNotDefined ${_expectedTarget})
  endif()
  if(TARGET ${_expectedTarget})
    list(APPEND _targetsDefined ${_expectedTarget})
  endif()
endforeach()
if("${_targetsDefined}" STREQUAL "${_expectedTargets}")
  unset(_targetsDefined)
  unset(_targetsNotDefined)
  unset(_expectedTargets)
  set(CMAKE_IMPORT_FILE_VERSION)
  cmake_policy(POP)
  return()
endif()
if(NOT "${_targetsDefined}" STREQUAL "")
  message(FATAL_ERROR "Some (but not all) targets in this export set were already defined.\nTargets Defined: ${_targetsDefined}\nTargets not yet defined: ${_targetsNotDefined}\n")
endif()
unset(_targetsDefined)
unset(_targetsNotDefined)
unset(_expectedTargets)


# Create imported target OpenAL::OpenAL
add_library(OpenAL::OpenAL SHARED IMPORTED)

set_target_properties(OpenAL::OpenAL PROPERTIES
  INTERFACE_INCLUDE_DIRECTORIES "F:/Dev5/Sources/Engine/External/OpenALSoft/include"
)

# Import target "OpenAL::OpenAL" for configuration "Debug"
set_property(TARGET OpenAL::OpenAL APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(OpenAL::OpenAL PROPERTIES
  IMPORTED_IMPLIB_DEBUG "F:/Dev5/Sources/Engine/External/OpenALSoft/build/Debug/OpenAL32.lib"
  IMPORTED_LOCATION_DEBUG "F:/Dev5/Sources/Engine/External/OpenALSoft/build/Debug/OpenAL32.dll"
  )

# Import target "OpenAL::OpenAL" for configuration "Release"
set_property(TARGET OpenAL::OpenAL APPEND PROPERTY IMPORTED_CONFIGURATIONS RELEASE)
set_target_properties(OpenAL::OpenAL PROPERTIES
  IMPORTED_IMPLIB_RELEASE "F:/Dev5/Sources/Engine/External/OpenALSoft/build/Release/OpenAL32.lib"
  IMPORTED_LOCATION_RELEASE "F:/Dev5/Sources/Engine/External/OpenALSoft/build/Release/OpenAL32.dll"
  )

# Import target "OpenAL::OpenAL" for configuration "MinSizeRel"
set_property(TARGET OpenAL::OpenAL APPEND PROPERTY IMPORTED_CONFIGURATIONS MINSIZEREL)
set_target_properties(OpenAL::OpenAL PROPERTIES
  IMPORTED_IMPLIB_MINSIZEREL "F:/Dev5/Sources/Engine/External/OpenALSoft/build/MinSizeRel/OpenAL32.lib"
  IMPORTED_LOCATION_MINSIZEREL "F:/Dev5/Sources/Engine/External/OpenALSoft/build/MinSizeRel/OpenAL32.dll"
  )

# Import target "OpenAL::OpenAL" for configuration "RelWithDebInfo"
set_property(TARGET OpenAL::OpenAL APPEND PROPERTY IMPORTED_CONFIGURATIONS RELWITHDEBINFO)
set_target_properties(OpenAL::OpenAL PROPERTIES
  IMPORTED_IMPLIB_RELWITHDEBINFO "F:/Dev5/Sources/Engine/External/OpenALSoft/build/RelWithDebInfo/OpenAL32.lib"
  IMPORTED_LOCATION_RELWITHDEBINFO "F:/Dev5/Sources/Engine/External/OpenALSoft/build/RelWithDebInfo/OpenAL32.dll"
  )

# This file does not depend on other imported targets which have
# been exported from the same project but in a separate export set.

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
cmake_policy(POP)
