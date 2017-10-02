#!/usr/local/bin/python
# coding: utf-8

'''
Created on 15/Sep/2017
Updated on 19/Sep/2017

@author: aplm

To run:
	- Need python 2.7 in environment
	- Also need sklearn installed

Command: python eval-ap.py -preds <predictions file> -truth <truth file> -output scores.txt
'''


import csv
import argparse
from sklearn.preprocessing import LabelEncoder
from sklearn.metrics import accuracy_score, confusion_matrix, f1_score, precision_recall_fscore_support, roc_auc_score
from sklearn import metrics, preprocessing

def report(le, y_test, y_pred, output_path):
        print("Classifation Report")
#         print(len(y_test))
#         print(len(y_pred))
#         print(type(y_test))
#         print(type(y_pred))
        print(y_test)
        print(y_pred)
#
#         print(le.classes_)
#         print(le.transform(le.classes_))

        target_names = le.classes_
        class_indices = {cls: idx for idx, cls in enumerate(le.classes_)}
        print(class_indices)

        print(metrics.classification_report(y_test, y_pred, target_names=target_names,
                                            labels=["" + str(class_indices[cls]) for cls in target_names]))

        print("============================================================")
        print("Confusion matrix")
        print("============================================================")
        print(target_names)
        print(confusion_matrix(y_test, y_pred,
            labels=[class_indices[cls] for cls in target_names]))

        precision_micro, recall_micro, fscore_micro, _ = \
        precision_recall_fscore_support(y_test, y_pred, average='micro', pos_label=None)

        precisions_macro, recalls_macro, fscore_macro, _ = \
        precision_recall_fscore_support(y_test, y_pred, average='macro', pos_label=None)

        precisions_weighted, recalls_weighted, fscore_weighted, _ =\
        precision_recall_fscore_support(y_test, y_pred, average='weighted', pos_label=None)

        measures = "p: %.4f r: %.4f f1: %.4f"
        print("Micro:  " + measures % (precision_micro, recall_micro, fscore_micro))
        print("Macro:  " + measures % (precisions_macro, recalls_macro, fscore_macro))
        print("Weight: " + measures % (precisions_weighted, recalls_weighted, fscore_weighted))

        print('Test Accuracy: %.4f' % accuracy_score(y_test, y_pred))
        #print('ROC AUC: %.3f' % roc_auc_score(y_true=y_test, y_score=y_pred))
        print("============================================================")

        with open(args.output, 'w') as f_output:
            f_output.write("accuracy:%.4f\n" % accuracy_score(y_test, y_pred))
            f_output.write("macro-f1:%.4f\n" % fscore_macro)
            # f_output.write("macro-precision:%.4f\n" % precisions_macro)
            # f_output.write("macro-recall:%.4f\n" % recalls_macro)

if __name__ == '__main__':

    parser = \
    argparse.ArgumentParser(description='Evaluate AP results from two csv files',
                            epilog=("Author: Adrián Pastor López-Monroy, Ph.D., " +
                                    "<alopezmonroy@uh.edu>, " +
                                    "RiTUAL Lab, " +
                                    "Department of Computer Science, " +
                                    "University of Houston, Texas USA.")
                            )

    parser.add_argument('-preds', help='Path for loading the results.')
    parser.add_argument('-truth', help='Path for loading the truth.')
    parser.add_argument('-output', help='Path for writing the evaluation.')

    args = parser.parse_args()

    print("Loading the txt files ...")
    with open(args.preds, 'rb') as f_pred:
        reader = csv.reader(f_pred)
        y_preds = list(reader)

    with open(args.truth, 'rb') as f_truth:
        reader = csv.reader(f_truth)
        y_truth = list(reader)

    #print y_truth

    y_preds_dict = {}
    y_truth_dict = {}

    #print y_preds
    #print y_truth

    for y_pred in y_preds:
        y_preds_dict[y_pred[0]] = y_pred[1]

    for y_true in y_truth:
        y_truth_dict[y_true[0]] = y_true[1]

    #y_preds = [y_pred[0] for y_pred in y_preds]
    #y_truth = [y_true[0] for y_true in y_truth]

    y_tru=[]
    y_te=[]
    for fname in y_truth_dict.keys():
        y_tru += [y_truth_dict[fname]]
        y_te += [y_preds_dict[fname]]

    #print y_te
    #print y_tru

    label_encoder = LabelEncoder()
    y_te = label_encoder.fit_transform(y_te)
    y_tru = label_encoder.fit_transform(y_tru)

    report(label_encoder, y_tru, y_te, args.output)
