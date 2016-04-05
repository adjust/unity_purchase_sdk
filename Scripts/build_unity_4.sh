#!/usr/bin/env bash

###############################################################################
# When copying from Backup folder instead of using files from Unity 5 assets, #
# it is because files used for Unity 5 are not compatible with Unity 4.       #
###############################################################################

########################################
# End script if one of the lines fails #
######################################## 
set -e

#####################################
# Clear any existing Unity 4 assets #
#####################################
rm -rf ./Unity4Assets

#####################################
# Recreate Unity 4 folder structure #
#####################################
mkdir -p ./Unity4Assets

mkdir -p ./Unity4Assets/AdjustPurchase
mkdir -p ./Unity4Assets/Plugins

mkdir -p "./Unity4Assets/AdjustPurchase/3rd Party"
mkdir -p ./Unity4Assets/AdjustPurchase/Android
mkdir -p ./Unity4Assets/AdjustPurchase/iOS
mkdir -p ./Unity4Assets/AdjustPurchase/Unity

mkdir -p ./Unity4Assets/Plugins/Android
mkdir -p ./Unity4Assets/Plugins/iOS

#################################
# Copy files to Unity 4 folders #
#################################

#########################
# AdjustPurchase folder #
#########################
cp ../Assets/AdjustPurchase/AdjustPurchase.cs ./Unity4Assets/AdjustPurchase/
cp ./Unity4Backup/AdjustPurchase.prefab ./Unity4Assets/AdjustPurchase/

####################
# 3rd Party folder #
####################
cp ../Assets/AdjustPurchase/3rd\ Party/SimpleJSON.cs ./Unity4Assets/AdjustPurchase/3rd\ Party/

#################################
# AdjustPurchase/Android folder #
#################################
cp ../Assets/AdjustPurchase/Android/AdjustPurchaseAndroid.cs ./Unity4Assets/AdjustPurchase/Android/

#############################
# AdjustPurchase/iOS folder #
#############################
cp ../Assets/AdjustPurchase/iOS/AdjustPurchaseiOS.cs ./Unity4Assets/AdjustPurchase/iOS/

###############################
# AdjustPurchase/Unity folder #
###############################
cp ../Assets/AdjustPurchase/Unity/* ./Unity4Assets/AdjustPurchase/Unity/

##########################
# Plugins/Android folder #
##########################
cp ../Assets/AdjustPurchase/Android/adjust-purchase-android.jar ./Unity4Assets/Plugins/Android/

######################
# Plugins/iOS folder #
######################
cp ../Assets/AdjustPurchase/iOS/*.h ./Unity4Assets/Plugins/iOS/
cp ../Assets/AdjustPurchase/iOS/*.mm ./Unity4Assets/Plugins/iOS/
cp ../Assets/AdjustPurchase/iOS/*.a ./Unity4Assets/Plugins/iOS/
