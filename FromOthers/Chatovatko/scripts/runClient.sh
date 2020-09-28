#!/bin/bash
#Find folder, where is script saved
SOURCE="${BASH_SOURCE[0]}"
while [ -h "$SOURCE" ]; do # resolve $SOURCE until the file is no longer a symlink
DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
SOURCE="$(readlink "$SOURCE")"
[[ $SOURCE != /* ]] && SOURCE="$DIR/$SOURCE" # if $SOURCE was a relative symlink, we need to resolve it relative to the path where the symlink file was located
      done
DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"

#Run the file
cd /tmp
temp=$$Chat
mkdir $temp

cd $DIR/..
cp -r Premy.Chatovatko /tmp/$temp
cd /tmp/$temp/Premy.Chatovatko/Premy.Chatovatko.Client.Console
dotnet run;
cd /tmp
rm -r $temp

