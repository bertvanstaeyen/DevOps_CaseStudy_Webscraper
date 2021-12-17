# DevOps & Security Case Study - Webscraper with Selenium and CI/CD GitHub Pipeline - Bert Van Staeyen

For this application you will need a chromdriver. This can be can downloaded at this url: http://chromedriver.storage.googleapis.com/index.html.
You will need to make sure you download the right version because it can differ depending on the version of chrome your running. To check the version click on the 3 dots next to your google profile picture. then go to settings. and in the end go to About Chrome. Now you now wich version you will need to install.

# Github actions deployment file:

```
name: build, test and upload artifact
```
When any changes happen to the repo this file will be reexecuted.
This file will build, publish and test the .sln file of this project
```
on:
  push:
  pull_request:
    branches: [ master ]
    paths:
    - 'DevOps_webscraper.sln'


env:
  DOTNET_VERSION: '5.0.301' # The .NET SDK version to use
```
All the defined jobs will now execute
```
jobs:
  build-and-test:

    name: build-and-test-${{matrix.os}}
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [windows-latest]

    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    - name: Install dependencies
      run: dotnet restore
      
    #- name: Build
    #  run: dotnet build --configuration Release --no-restore
    
    - name: publish
      run: |
          dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true
    
    #- name: Test
    #  run: dotnet test --no-restore --verbosity normal
```
      
Upload artifact from defined path
```
    - name: Upload a Build Artifact
      uses: actions/upload-artifact@v2.2.2
      with:
        # Artifact name
        name: Webscraper #.zip will be added automatically
        path: "./DevOps_webscraper/bin/Release/net5.0/win-x64/publish/**.exe"
```
