MVNDIR = ./sdk/AdjustPurchase
JARINDIR = ./sdk/AdjustPurchase/target
JAROUTDIR = ../../Assets/AdjustPurchase/Android

default: copy

build: clean
	cd $(MVNDIR); mvn package

clean:
	cd $(MVNDIR); mvn clean

copy: build
	rm $(JAROUTDIR)/adjust-purchase-android*; \
	cp $(JARINDIR)/adjust-purchase-android-*.*.*.jar $(JAROUTDIR); \
	rm $(JAROUTDIR)/*-javadoc.jar; \
	rm $(JAROUTDIR)/*-sources.jar; \
	mv $(JAROUTDIR)/adjust-purchase-android-*.*.*.jar $(JAROUTDIR)/adjust-purchase-android.jar
