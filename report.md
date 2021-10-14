* 5 minutes after start-up
```sh
$ cat /proc/$(pidof highmem-test)/status | grep Rss
RssAnon:           10800 kB
RssFile:           34596 kB
RssShmem:              0 kB
```

* 3 hour after start-up
```sh
$ cat /proc/$(pidof highmem-test)/status | grep Rss
RssAnon:           11396 kB
RssFile:           35068 kB
RssShmem:              0 kB
```

* 24 hour after start-up
```
$ cat /proc/$(pidof highmem-test)/status | grep Rss
RssAnon:           11540 kB
RssFile:           35068 kB
RssShmem:              0 kB
```
