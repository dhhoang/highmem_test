* 5 minutes after start-up
```sh
$ cat /proc/$(pidof highmem-test)/status | grep Rss
RssAnon:            8812 kB
RssFile:           24680 kB
RssShmem:              0 kB
```

* 3 hour after start-up
```sh
$ cat /proc/$(pidof highmem-test)/status | grep Rss
RssAnon:            9432 kB
RssFile:           25188 kB
RssShmem:              0 kB
```

* 24 hour after start-up
```
$ cat /proc/$(pidof highmem-test)/status | grep Rss
RssAnon:            9624 kB
RssFile:           25188 kB
RssShmem:              0 kB
```
