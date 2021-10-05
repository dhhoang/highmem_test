## Memory usage testing for .NET 6 on Linux
### Steps
* Clone this repo
* Compile: `dotnet publish -c Release highmem-test.csproj -r linux-x64 --self-contained`
* Change the `ExecStart` in the `highmem_test.service` file to point to the compiled executable.
* Copy `highmem_test.service` to systemd directory `/etc/systemd/system/`
* Run `sudo systemctl daemon-reload && sudo systemctl restart highmem_test.service`
* Observe memory usage `cat /proc/$(pidof highmem-test)/status | grep Rss`
