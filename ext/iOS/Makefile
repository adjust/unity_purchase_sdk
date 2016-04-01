SRCDIR = ./sdk
LIBOUTDIR = ../../Assets/AdjustPurchase/iOS
STATICFRAMEWORK = $(SRCDIR)/Frameworks/Static/AdjustPurchaseSdk.framework

default: copy

build:	
	cd $(SRCDIR); \
	xcodebuild -target AdjustPurchaseStatic -configuration Release

copy: build
	cp $(STATICFRAMEWORK)/Versions/A/AdjustPurchaseSdk $(LIBOUTDIR)/AdjustPurchaseSdk.a; \
	cp $(STATICFRAMEWORK)/Versions/A/Headers/* $(LIBOUTDIR);
